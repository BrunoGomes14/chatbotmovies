using Twilio;
using Twilio.Rest.Api.V2010.Account;
using api.Models;
using api.Services;
using Twilio.Types;

namespace api.Interfaces.Message
{
    public class WhatsappSendMessage : ISendMessage
    {
        private readonly string AccountSid;
        private readonly string AuthToken;
        private readonly string FromNumber;
        private readonly Stack<(string, string, Uri?)> AfterMessages;
    
        public WhatsappSendMessage(AppSettings config)
        {
            AccountSid = config.TwilioConfig!.Sid.FromBase64();
            AuthToken = config.TwilioConfig.Token.FromBase64();
            FromNumber =  config.TwilioConfig.Number;
            AfterMessages = new();

            TwilioClient.Init(AccountSid, AuthToken);
        }

        public void Send(string id, string body, Uri? media)
        {
            lock ("send")
            {
                SendMessage(
                    new PhoneNumber(FromNumber),
                    new Twilio.Types.PhoneNumber(id),
                    body,
                    media);

                ProcessAfter();
            }
        }

        public void SendAfter(string id, string body, Uri? media = null)
        {
            lock ("add")
            {
                AfterMessages.Push((id, body, media));
            }
        }

        private void SendMessage(
            PhoneNumber phoneNumberFrom,
            PhoneNumber phoneNumberTo,
            string body,
            Uri? uri)
        {
            var uris = new List<Uri>();
            if (uri != null)
                uris.Add(uri);

             MessageResource.Create(
                from: phoneNumberFrom,
                to: phoneNumberTo,
                body: body,
                mediaUrl: uris
            );
        }

        private void ProcessAfter()
        {
            if (AfterMessages.Count == 0)
                return;

            var item = AfterMessages.Peek();

            Thread.Sleep(1000);

            SendMessage(
                new PhoneNumber(FromNumber),
                new PhoneNumber(item.Item1),
                item.Item2,
                item.Item3);

            AfterMessages.Pop();

            if (AfterMessages.Count > 0)
            {
                Console.WriteLine("ATENCAO: ACUMULOS DE PILHA OCORRENDO!!!!");
                AfterMessages.Clear();
            }
        }
    }
}