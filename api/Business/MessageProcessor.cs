using api.Interfaces;
using api.Models;

namespace api.Business
{
    public class MessageProcessor
    {
        private SendMessage _sendMessage { get; set; }

        public MessageProcessor(SendMessage sendMessage)
        {
            _sendMessage = sendMessage;
        }

        public async Task Process(Message message)
        {
            var peopleStatus = await VerifyPeopleStatus(message.Id);

            if (peopleStatus == PeopleStatus.InitConversation)
                await InitConversation(message);
            else 
                await ContinueConversation(message, peopleStatus);
            
        }

        public async Task<PeopleStatus> VerifyPeopleStatus(string id)
        {
            return PeopleStatus.InitConversation;
        }

        public async Task InitConversation(Message message)
        {
            // insere no banco a mensagem

            // insere no banco o status 

            // envia a mensagem inicial
            _sendMessage.Send(message.Id, $"Olá {message.UserName} 😊\nO que deseja fazer agora??\n\nTemos duas opções até o momento:\n\n1 - Procurar onde assistir o filme\n2 - Descobrir um filme novo para assistir\n");
        }

        public async Task ContinueConversation(Message message, PeopleStatus peopleStatus)
        {
            switch (peopleStatus)
            {
                case PeopleStatus.WaitingChoseFunction:
                    break;
                default:
                    break;
            }
        }

        public async Task FindMovie(string movie)
        {

        }

        public async Task ContinueAskMovieTypes()
        {

        }
    }
}