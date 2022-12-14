using api.Business;
using api.Interfaces.Message;
using api.Models;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace api.Services
{
    public class TelegramConfiguration
    {
        private readonly MessageProcessor _messageProcessor;
        private readonly TelegramSendMessage _sendMessage;
        private readonly TelegramBotClient _client;

        public TelegramConfiguration(
            MessageProcessor messageProcessor,
            AppSettings appSettings,
            TelegramSendMessage sendMessage)
        {
            _messageProcessor = messageProcessor;
            _sendMessage = sendMessage;
            _client = new TelegramBotClient(appSettings.TelegramKey);
            _sendMessage.Setup(_client);
        }

        public void Setup()
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                }
            };

            _client.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
        }

        private Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message!.Type == MessageType.Text || update.Message.Type == MessageType.Location)
                {
                    _messageProcessor!.Setup(
                         _sendMessage,
                         new Models.Message
                         {
                             Id = update.Message.Chat.Id.ToString(),
                             Content = update.Message.Text,
                             UserName = update.Message.From?.FirstName ?? "Humanx",
                             Latitude = Convert.ToDecimal(update.Message.Location?.Latitude, new CultureInfo("en-US")),
                             Longitude = Convert.ToDecimal(update.Message.Location?.Longitude, new CultureInfo("en-US")),
                         },
                         Models.Plataform.TELEGRAM);

                    await _messageProcessor.Process();
                }
            }
        }

    }
}
