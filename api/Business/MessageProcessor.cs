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

        private Message _message;
        private ISendMessage _sendMessage;

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

        public void Setup(ISendMessage sendMessage, Message message)
        {
            _sendMessage = sendMessage;
            _message = message;
        }

        public async Task Process()
        {
            var peopleStatus = await VerifyPeopleStatus(_message.Id);

            try
            {
                if (peopleStatus == PeopleStatus.InitConversation || peopleStatus == PeopleStatus.RestartConversation)
                    await InitConversation(peopleStatus);
                else
                    await ContinueConversation(peopleStatus);
            }
            catch (NotUnderstandException ex)
            {
                await _database.UpdateStatus(_message.Id, peopleStatus);
                _sendMessage.Send(_message.Id, ex.Reason ?? Messages.NotUnderstand);
            }
            catch (Exception)
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
                status == PeopleStatus.InitConversation ? Messages.StartMessage : Messages.StartMessage,
                _message.UserName);

            var history = new ConversationHistory() {
                From = "server",
                Sent = message
            };

            var list = new List<ConversationHistory>();
            list.Add(history);

            // insere no banco o status 
            await _database.AddStatus(_message.Id, PeopleStatus.WaitingChoseFunction, Plataform.WHATSAPP);

            // envia a mensagem inicial
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
                case PeopleStatus.WatingDecideAfterFindLocation:
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(content))
                _sendMessage.Send(_message.Id, content, uri);
        }

        private async Task<string> DetectDecisionAndContinue()
        {
            var option = _translator.ToListOption(_message.Content!, TranslationOptions.ChooseFuncOptions());

            if (option == TranslateAnswer.NotFound)
                throw new NotUnderstandException();

            switch (option)
            {
                case 0: // ENCONTRAR FILME
                    await _database.UpdateStatus(_message.Id, PeopleStatus.WaintingWriteMovieToFind);
                    return Messages.SearchMovie;
                case 1: // LOCALIZAR CINEMA
                    await _database.UpdateStatus(_message.Id, PeopleStatus.WaitingSendLocation);
                    return Messages.SendLocation;
                default:
                    throw new NotUnderstandException();
            }
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
            var banner = $"https://image.tmdb.org/t/p/w500{result.movie.poster_path}";

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

            _ = Task.Run(async () =>
            {
                Thread.Sleep(10);
                _sendMessage.Send(_message.Id, Messages.MeetMovie);
                await _database.UpdateStatus(_message.Id, PeopleStatus.WaintingWriteMovieToFind);
            });

            return (formatted, new Uri(theaterResult.Theater.images.First(x => x.type.ToLower() == "logo").url));
        }


        public void DetectDecisionAndContinueAfterMovie()
        {
            var result = _translator.ToListOption(_message.Content, TranslationOptions.ChooseFuncOptionsAfterMovie(), true);
        }
    }
}