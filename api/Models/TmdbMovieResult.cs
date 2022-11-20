namespace api.Models
{
    public class TmdbMovieResult
    {
        public TmdbMovie movie { get; set; } = default!;
        public TmdbStream stream { get; set; } = default!;
        public bool isTrust { get; set; } = default!;
    }
}
