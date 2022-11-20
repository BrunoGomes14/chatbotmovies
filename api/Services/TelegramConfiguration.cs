using api.Business;
using api.Interfaces.Message;

namespace api.Services
{
    public class TelegramConfiguration
    {
        private readonly MessageProcessor _messageProcessor;
        private readonly TelegramSendMessage _sendMessage;

        public TelegramConfiguration(
            MessageProcessor messageProcessor, 
            TelegramSendMessage sendMessage)
        {
            _messageProcessor = messageProcessor;
            _sendMessage = sendMessage;
        }

        public void Setup()
        {
            // configura o telegram aqui


            // trocar null por instancia do client
            //_sendMessage.Setup(null);
        }

        public async Task onMessage()
        {
            _messageProcessor.Setup(
                _sendMessage,
                new Models.Message
                {
                    Id = "",
                    Content = "", 
                    UserName = ""
                },
                Models.Plataform.TELEGRAM);

            await _messageProcessor.Process();
        }
    }
}
