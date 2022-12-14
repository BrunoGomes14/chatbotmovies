namespace api.Models
{
    public class Genre
    {
        public int id { get; set; } = default!;
        public string name { get; set; } = default!;
    }

    public class ProductionCompany
    {
        public int id { get; set; } = default!;
        public string logo_path { get; set; } = default!;
        public string name { get; set; } = default!;
        public string origin_country { get; set; } = default!;
    }

    public class ProductionCountry
    {
        public string iso_3166_1 { get; set; } = default!;
        public string name { get; set; } = default!;
    }

    public class TmdbMovie
    {
        public bool adult { get; set; } = default!;
        public string backdrop_path { get; set; } = default!;
        public object belongs_to_collection { get; set; } = default!;
        public long budget { get; set; } = default!;
        public List<Genre> genres { get; set; } = default!;
        public string homepage { get; set; } = default!;
        public int id { get; set; } = default!;
        public string imdb_id { get; set; } = default!;
        public string original_language { get; set; } = default!;
        public string original_title { get; set; } = default!;
        public string overview { get; set; } = default!;
        public double popularity { get; set; } = default!;
        public string poster_path { get; set; } = default!;
        public List<ProductionCompany> production_companies { get; set; } = default!;
        public List<ProductionCountry> production_countries { get; set; } = default!;
        public DateTime release_date { get; set; } = default!;
        public long revenue { get; set; } = default!;
        public int runtime { get; set; } = default!;
        public List<SpokenLanguage> spoken_languages { get; set; } = default!;
        public string status { get; set; } = default!;
        public string tagline { get; set; } = default!;
        public string title { get; set; } = default!;
        public bool video { get; set; } = default!;
        public double vote_average { get; set; } = default!;
        public int vote_count { get; set; } = default!;
    }

    public class SpokenLanguage
    {
        public string english_name { get; set; } = default!;
        public string iso_639_1 { get; set; } = default!;
        public string name { get; set; } = default!;
    }


}
