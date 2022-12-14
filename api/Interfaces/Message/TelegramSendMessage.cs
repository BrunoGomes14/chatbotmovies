using api.Business;
using api.Models;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace api.Interfaces.Message
{
    public class TelegramSendMessage : ISendMessage
    {
        private readonly Stack<(string, string, Uri?)> AfterMessages = new();
        private TelegramBotClient? _client;

        public void Setup(TelegramBotClient client)
        {
            _client = client;
        }

        public void Send(string id, string body, Uri? media = null)
        {
            lock ("send")
            {
                var task = SendMessage(id, body, media);
                task.Wait();


                task = ProcessAfter();
                task.Wait();
            }
        }

        public void SendAfter(string id, string body, Uri? media = null)
        {
            lock ("add")
            {
                AfterMessages.Push((id, body, media));
            }
        }

        private async Task SendMessage(string id, string body, Uri? media = null)
        {
            var chatId = new ChatId(Convert.ToInt64(id));

            if (media != null)
            {
                try
                {
                    await _client!.SendPhotoAsync(
                        chatId, media.ToString().Replace("/original/", "/w200/"));
                }
                catch (Exception)
                {
                }
            }
            
            await _client!.SendTextMessageAsync(
                chatId, 
                body,
                parseMode: ParseMode.Markdown);
        }

        private async Task ProcessAfter()
        {
            if (AfterMessages.Count == 0)
                return;

            var item = AfterMessages.Peek();

            Thread.Sleep(1000);

            await SendMessage(item.Item1, item.Item2, item.Item3);

            AfterMessages.Pop();

            if (AfterMessages.Count > 0)
            {
                Console.WriteLine("ATENCAO: ACUMULOS DE PILHA OCORRENDO!!!!");
                AfterMessages.Clear();
            }
        }
        
    }
}
