namespace api.Models
{
    public class NearestTheaterResult
    {
        public NearestTheaterResult() { }
        public NearestTheaterResult(string message, bool sucess)
        {
            Message = message;
            Sucess = sucess;
        }


        public string Message { get; set; }
        public bool Sucess { get; set; }
        public Item Theater { get; set; }
        public List<IngressoMovie> MoviesAvaible { get; set; }
    }
}
