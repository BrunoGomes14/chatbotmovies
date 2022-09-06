namespace api.Interfaces
{
    public interface SendMessage
    {
        void Send(string id, string body, Uri? media = null);       
    } 
}