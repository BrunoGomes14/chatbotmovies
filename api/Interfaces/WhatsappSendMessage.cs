using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace api.Interfaces
{
    public class WhatsappSendMessage : SendMessage
    {
        private readonly string AccountSid;
        private readonly string AuthToken;
        private readonly string FromNumber;

        public WhatsappSendMessage()
        {
            AccountSid = "AC08e0fd6a0434d2da233bd0860ad8d169";
            AuthToken = "7eb4f3f5d9c79c919eb9428110cacd06";
            FromNumber =  "whatsapp:+14155238886";

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