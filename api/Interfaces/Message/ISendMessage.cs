namespace api.Interfaces.Message
{
    public interface ISendMessage
    {
        void Send(string id, string body, Uri? media = null);
        void SendAfter(string id, string body, Uri? media = null);
    } 
}