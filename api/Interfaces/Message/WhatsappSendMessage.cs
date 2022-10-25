using Twilio;
using Twilio.Rest.Api.V2010.Account;
using api.Models;
using api.Services;

namespace api.Interfaces.Message
{
    public class WhatsappSendMessage : ISendMessage
    {
        private readonly string AccountSid;
        private readonly string AuthToken;
        private readonly string FromNumber;
    
        public WhatsappSendMessage(AppSettings config)
        {
            AccountSid = config.TwilioConfig.Sid.FromBase64();
            AuthToken = config.TwilioConfig.Token.FromBase64();
            FromNumber =  config.TwilioConfig.Number;

            TwilioClient.Init(AccountSid, AuthToken);
        }

        public void Send(string id, string body, Uri? media)
        {
            lock ("send")
            {
                var uris = new List<Uri>();
                if (media != null)
                    uris.Add(media);

                MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber(FromNumber),
                    to: new Twilio.Types.PhoneNumber(id),
                    body: body,
                    mediaUrl: uris
                );
            }
        }
    }
}