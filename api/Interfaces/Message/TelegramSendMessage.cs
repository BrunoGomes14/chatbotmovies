namespace api.Interfaces.Message
{
    public class TelegramSendMessage : ISendMessage
    {
        private readonly Stack<(string, string, Uri?)> AfterMessages;
        private object _client;

        public void Setup(object client)
        {
            _client = client;
        }


        public void Send(string id, string body, Uri? media = null)
        {
            lock ("send")
            {
                SendMessage();

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

        private void SendMessage()
        {
            // implementar envio de mensagem
            //_client.SendMessage();
        }

        private void ProcessAfter()
        {
            if (AfterMessages.Count == 0)
                return;

            var item = AfterMessages.Peek();

            Thread.Sleep(1000);

            SendMessage();

            AfterMessages.Pop();

            if (AfterMessages.Count > 0)
            {
                Console.WriteLine("ATENCAO: ACUMULOS DE PILHA OCORRENDO!!!!");
                AfterMessages.Clear();
            }
        }
    }
}
