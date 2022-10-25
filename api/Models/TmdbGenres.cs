namespace api.Models
{
    public class TmdbGenre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TmdbGenres
    {
        public List<TmdbGenre> genres { get; set; }
    }
}
