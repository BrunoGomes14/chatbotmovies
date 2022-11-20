using api.Interfaces.Message;
using api.Interfaces.Data;
using api.Models;
using api.Models.Exception;
using api.Services;
using System.Text;

namespace api.Business
{
    public class MessageProcessor
    {
        private readonly IChatDatabase _database;
        private readonly TranslateAnswer _translator;
        private readonly GetNearestTheater _getNearestTheater;
        private readonly GetMoviesInfo _getMoviesInfo;

        private Message _message = new();
        private ISendMessage _sendMessage = default!;
        private Plataform _plataform;

        public MessageProcessor(
            IChatDatabase database,
            TranslateAnswer translator,
            GetNearestTheater getNearestTheater,
            GetMoviesInfo getMoviesInfo)
        {
            _translator = translator;
            _database = database;
            _getNearestTheater = getNearestTheater;
            _getMoviesInfo = getMoviesInfo;
        }

        public void Setup(ISendMessage sendMessage, Message message, Plataform plataform)
        {
            _sendMessage = sendMessage;
            _message = message;
            _plataform = plataform;

            _translator.Setup(_message.Content!);
        }

        public async Task Process()
        {
            var peopleStatus = await VerifyPeopleStatus(_message.Id);

            try
            {
                if (peopleStatus == PeopleStatus.InitConversation || peopleStatus == PeopleStatus.RestartConversation)
                    await InitConversation(peopleStatus);
                else if (peopleStatus == PeopleStatus.Processing)
                    return;
                else
                    await ContinueConversation(peopleStatus);
            }
            catch (NotUnderstandException ex)
            {
                await _database.UpdateStatus(_message.Id, peopleStatus);
                _sendMessage.Send(_message.Id, ex.Reason ?? Messages.NotUnderstand);
            }
            catch (Exception err)
            {
                await _database.UpdateStatus(_message.Id, peopleStatus);
                _sendMessage.Send(_message.Id, Messages.Error);
            }
        }

        private async Task<PeopleStatus> VerifyPeopleStatus(string id)
        {
            var conversation = _database.GetConversation(id);

            if (conversation != null)
            {
                var time = DateTime.Now - conversation.LastReceive;
                if (time.TotalMinutes > 10)
                {
                    await _database.UpdateStatus(conversation.PeopleId, PeopleStatus.FinishedByApplication);
                }
                else
                {
                    return (PeopleStatus)conversation.Status;
                }
            }

            return conversation != null 
                                ? PeopleStatus.RestartConversation
                                : PeopleStatus.InitConversation;
        }

        private async Task InitConversation(PeopleStatus status)
        {
            string message = string.Format(
                status == PeopleStatus.InitConversation
                ? Messages.StartMessage
                : Messages.RestartMessage,
                _message.UserName);

            await _database.AddStatus(_message.Id, PeopleStatus.WaitingChoseFunction, _plataform);
            _sendMessage.Send(_message.Id, message);
        }

        private async Task ContinueConversation(PeopleStatus peopleStatus)
        {
            string content = string.Empty;
            Uri? uri = null;

            await _database.UpdateStatus(_message.Id, PeopleStatus.Processing);

            switch (peopleStatus)
            {
                case PeopleStatus.WaitingChoseFunction:
                    content = await DetectDecisionAndContinue();
                    break;
                case PeopleStatus.WaintingWriteMovieToFind:
                    (content, uri) = await FindMovie();
                    break;
                case PeopleStatus.WaitingAnswerQuestion:
                    //await ContinueDiscover();
                    break;
                case PeopleStatus.WaitingSendLocation:
                    (content, _) = await FindLocation();
                    break;
                case PeopleStatus.WaitingChoseFunctionAfterMovie:
                    DetectDecisionAndContinueAfterMovie();
                    break;
                case PeopleStatus.WaitingConfirmSortMovie:
                    (content, uri) = await VerifyAnswerSort();
                    break;
                default:
                    await VerifyHowProcede();
                    break;
            }

            if (!string.IsNullOrEmpty(content))
                _sendMessage.Send(_message.Id, content, uri);
        }

        private async Task<string> DetectDecisionAndContinue()
        {
            var option = _translator.ToListOption(TranslationOptions.ChooseFuncOptions());

            switch (option)
            {
                case 0: // ENCONTRAR FILME
                    await _database.UpdateStatus(_message.Id, PeopleStatus.WaintingWriteMovieToFind);
                    return Messages.SearchMovie;
                case 1: // SORTEAR FILME
                    await _database.UpdateStatus(_message.Id, PeopleStatus.WaitingConfirmSortMovie);
                    return Messages.SortMovie;
                case 2: // LOCALIZAR CINEMA
                    await _database.UpdateStatus(_message.Id, PeopleStatus.WaitingSendLocation);
                    return Messages.SendLocation;
                default:
                    await AnyTimeDecision(option);
                    break;
            }

            return string.Empty;
        }

        private async Task<(string, Uri)> FindMovie()
        {
            string? content = _message.Content;
            if (string.IsNullOrEmpty(content) || content.Trim().Length < 3)
            {
                throw new NotUnderstandException("Calma aí! Esse filme que você digitou tá muito pequeno, é isso mesmo?");
            }

            var result = await _getMoviesInfo.SearchMovie(content);
            var formatted = _getMoviesInfo.FormatResult(result);
            var banner = $"https://image.tmdb.org/t/p/original{result.movie.poster_path}";

            await _database.UpdateStatus(_message.Id, PeopleStatus.WaitingChoseFunctionAfterMovie);
            await _database.InsertResult(_message.Id, result.ToJson(), (int)PeopleStatus.WaintingWriteMovieToFind);

            return (formatted, new Uri(banner));
        }

        private async Task<(string, Uri)> FindLocation()
        {

            string? cep = _message.Content;
            if (!string.IsNullOrEmpty(_message.Content))
            {
                cep = _translator.GetCEPInText(_message.Content);
            }

            if (cep == null && (_message.Latitude == 0))
                throw new NotUnderstandException();


            var task = _getNearestTheater.Execute(_message.Latitude, _message.Longitude, cep);
            _sendMessage.Send(_message.Id, Messages.FindingLocation);

            var theaterResult = await task;
            var formatted = _getNearestTheater.FormatResult(theaterResult);

            _sendMessage.SendAfter(_message.Id, Messages.MeetMovie);
            await _database.UpdateStatus(_message.Id, PeopleStatus.WaintingWriteMovieToFind);


            return (formatted, new Uri(theaterResult.Theater.images.First(x => x.type.ToLower() == "logo").url));
        }


        private void DetectDecisionAndContinueAfterMovie()
        {
            var result = _translator.ToListOption(TranslationOptions.ChooseFuncOptionsAfterMovie(), true);

            switch (result)
            {
                default:
                    AnyTimeDecision(result);
                    break;
            }
        }

        private async Task<(string, Uri)> VerifyAnswerSort()
        {
            (string, Uri) tuple = (string.Empty, default!);

            var result = _translator.ToListOption(TranslationOptions.ChooseFuncOptionsSortMovie());
            switch (result)
            {
                case 0:
                    tuple = await SortMovie();
                    break;
                default:
                    await AnyTimeDecision(result);
                    break;
            }

            return tuple;
        }

        private async Task<(string, Uri)> SortMovie()
        {
            var result = await _getMoviesInfo.GetSortedMovie();
            var formatted = _getMoviesInfo.FormatResult(result);
            var banner = $"https://image.tmdb.org/t/p/original{result.movie.poster_path}";

            await _database.UpdateStatus(_message.Id, PeopleStatus.WaitingChoseFunctionAfterMovie);
            await _database.InsertResult(_message.Id, result.ToJson(), (int)PeopleStatus.WaintingWriteMovieToFind);

            return (formatted, new Uri(banner));
        }

        private Task AnyTimeDecision(int decision) => decision switch 
        {
            TranslateAnswer.ExitDetected => ExitProcess(),
            TranslateAnswer.RestartDetected => RestartProcess(),
            _ => throw new NotUnderstandException(),
            
        };

        private async Task ExitProcess()
        {
            await _database.UpdateStatus(_message.Id, PeopleStatus.FinishedByUser);
            _sendMessage.Send(_message.Id, Messages.FinishedByChoice);
        }

        private async Task RestartProcess(bool sendApologize = false)
        {
            if (sendApologize)
                _sendMessage.Send(_message.Id, Messages.NotUnderstandStatus);

            await _database.UpdateStatus(_message.Id, PeopleStatus.FinishedForRestart);

            if (!sendApologize)
                await InitConversation(PeopleStatus.RestartConversation);
        }
        
        private async Task VerifyHowProcede()
        {
            int decision = _translator.CheckAnyTimeDecision();

            if (decision == TranslateAnswer.NotFound)
                throw new NotUnderstandException();

            await AnyTimeDecision(decision);
        }
    }
}