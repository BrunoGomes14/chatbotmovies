namespace api.Models
{
    public class TmdbMovieResult
    {
        public TmdbMovie movie { get; set; }
        public TmdbStream stream { get; set; }
        public bool isTrust { get; set; }
    }
}
