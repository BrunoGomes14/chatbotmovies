namespace api.Models
{
    public class Flatrate
    {
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
        public int display_priority { get; set; }
    }

    public class Results
    {
        public TmdbStreamResult BR { get; set; }
    }

    public class TmdbStreamResult
    {
        public string link { get; set; }
        public List<Flatrate> flatrate { get; set; }
    }

    public class TmdbStream
    {
        public int id { get; set; }
        public Results results { get; set; }
    }
}
