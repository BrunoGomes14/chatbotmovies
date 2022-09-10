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
    
        public WhatsappSendMessage(TwilioConfig config)
        {
            AccountSid = config.Sid.FromBase64();
            AuthToken = config.Token.FromBase64();
            FromNumber =  config.Number;

            TwilioClient.Init(AccountSid, AuthToken);
        }

        public void Send(string id, string body, Uri? media)
        {
            MessageResource.Create(
                from: new Twilio.Types.PhoneNumber(FromNumber), 
                to: new Twilio.Types.PhoneNumber(id),
                body: body
            );
        }
    }
}