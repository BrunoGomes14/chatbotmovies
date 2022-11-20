namespace api.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class TmdbResult
    {
        public bool adult { get; set; } = default!;
        public string backdrop_path { get; set; } = default!;
        public List<int> genre_ids { get; set; } = default!;
        public int id { get; set; } = default!;
        public string original_language { get; set; } = default!;
        public string original_title { get; set; } = default!;
        public string overview { get; set; } = default!;
        public double popularity { get; set; } = default!;
        public string poster_path { get; set; } = default!;
        public string release_date { get; set; } = default!;
        public string title { get; set; } = default!;
        public bool video { get; set; } = default!;
        public double vote_average { get; set; } = default!;
        public int vote_count { get; set; } = default!;
    }

    public class TbmdbSearchResult
    {
        public int page { get; set; } = default!;
        public List<TmdbResult> results { get; set; } = default!;
        public int total_pages { get; set; } = default!;
        public int total_results { get; set; } = default!;
    }


}
