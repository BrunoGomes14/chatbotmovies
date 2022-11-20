namespace api.Models
{
    public class Flatrate
    {
        public string logo_path { get; set; } = default!;
        public int provider_id { get; set; } = default!;
        public string provider_name { get; set; } = default!;
        public int display_priority { get; set; } = default!;
    }

    public class Results
    {
        public TmdbStreamResult BR { get; set; } = default!;
    }

    public class TmdbStreamResult
    {
        public string link { get; set; } = default!;
        public List<Flatrate> flatrate { get; set; } = default!;
    }

    public class TmdbStream
    {
        public int id { get; set; } = default!;
        public Results results { get; set; } = default!;
    }
}
