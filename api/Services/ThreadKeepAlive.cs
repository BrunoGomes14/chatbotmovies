namespace api.Services
{
    public class ThreadKeepAlive
    {
        private readonly Thread thread;

        public ThreadKeepAlive()
        {
            ThreadStart threadStart = new ThreadStart(this.KeepAlive);
            thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
        }

        private void KeepAlive()
        {
            while (true)
            {
                HttpClient client = new HttpClient();
                _ = client.GetAsync("https://chatbotuninove.herokuapp.com/WhatsappReceiver/test")
                          .Result
                          .Content
                          .ReadAsStringAsync()
                          .Result;

                // 20 minutos
                Thread.Sleep(1200000);
            }
        }
    }
}
