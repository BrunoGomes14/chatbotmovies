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


        public string Message { get; set; } = default!;
        public bool Sucess { get; set; } = default!;
        public Item Theater { get; set; } = default!;
        public List<IngressoMovie> MoviesAvaible { get; set; } = default!;
    }
}
