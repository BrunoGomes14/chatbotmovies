using api.Interfaces.Message;
using api.Interfaces.Data;
using api.Models;
using api.Services;

namespace api.Business
{
    public class MessageProcessor
    {
        private readonly ISendMessage _sendMessage;
        private readonly Message _message;
        private readonly TranslateAnswer _translator;
        private readonly IChatDatabase _database;

        public MessageProcessor(ISendMessage sendMessage, Message message)
        {
            _sendMessage = sendMessage;
            _message = message;
            _translator = new TranslateAnswer();
            _database = new ChatDatabase();
        }

        public async Task Process()
        {
            var peopleStatus = await VerifyPeopleStatus(_message.Id);

            if (peopleStatus == PeopleStatus.InitConversation)
                await InitConversation();
            else 
                await ContinueConversation(peopleStatus);
            
        }

        private async Task<PeopleStatus> VerifyPeopleStatus(string id)
        {
            //int? iResult = await _database.GetStatus(id);

            //if (iResult.HasValue)
            //    return (PeopleStatus)iResult.Value;

            return PeopleStatus.InitConversation;
        }

        private async Task InitConversation()
        {
            string message = string.Format(Messages.StartMessage, _message.UserName);

            // var history = new ConversationHistory() {
            //     From = "server",
            //     Body = message
            // };

            // new List<ConversationHistory>().Add(history);

            // // insere no banco o status 
            // await _database.AddStatus(_message.Id, PeopleStatus.WaitingChoseFunction, Plataform.WHATSAPP, "");

            // envia a mensagem inicial
            _sendMessage.Send(_message.Id, message);
        }

        private async Task ContinueConversation(PeopleStatus peopleStatus)
        {
            switch (peopleStatus)
            {
                case PeopleStatus.WaitingChoseFunction:
                    await DetectDecisionAndContinue();
                    break;
                case PeopleStatus.WaintingWriteMovieToFind:
                    await FindMovie();
                    break;
                case PeopleStatus.WaitingAnswerQuestion:
                    await ContinueDiscover();
                    break;
                default:
                    _sendMessage.Send(_message.Id, "Ops, não consegui entender sua respota. Poderia reformular ela?🤔");
                    break;
            }
        }

        private async Task DetectDecisionAndContinue()
        {
            await _database.GetConversation(_message.Id);
        }

        private async Task FindMovie()
        {
            await _database.GetConversation(_message.Id);
        }

        private async Task ContinueDiscover()
        {
            await _database.GetConversation(_message.Id);
        }
    }
}