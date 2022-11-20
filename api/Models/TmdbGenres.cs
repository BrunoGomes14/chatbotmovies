namespace api.Models
{
    public class TmdbGenre
    {
        public int id { get; set; } = default!;
        public string name { get; set; } = default!;
    }

    public class TmdbGenres
    {
        public List<TmdbGenre> genres { get; set; } = default!;
    }
}
