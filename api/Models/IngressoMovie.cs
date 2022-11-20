namespace api.Models
{
    public class Date
    {
        public DateTime? localDate { get; set; } = default!;
        public bool? isToday { get; set; } = default!;
        public string dayOfWeek { get; set; } = default!;
        public string dayAndMonth { get; set; } = default!;
        public string hour { get; set; } = default!;
        public string year { get; set; } = default!;
    }

    public class MovieImage
    {
        public string url { get; set; } = default!;
        public string type { get; set; } = default!;
    }

    public class Movie
    {
        public string id { get; set; } = default!;
        public string title { get; set; } = default!;
        public string originalTitle { get; set; } = default!;
        public string movieIdUrl { get; set; } = default!;
        public bool inPreSale { get; set; } = default!;
        public bool isReexhibition { get; set; } = default!;
        public string duration { get; set; } = default!;
        public string contentRating { get; set; } = default!;
        public string distributor { get; set; } = default!;
        public string urlKey { get; set; } = default!;
        public string siteURL { get; set; } = default!;
        public string nationalSiteURL { get; set; } = default!;
        public string siteURLByTheater { get; set; } = default!;
        public string nationalSiteURLByTheater { get; set; } = default!;
        public string boxOfficeId { get; set; } = default!;
        public string ancineId { get; set; } = default!;
        public List<MovieImage> images { get; set; } = default!;
        public List<Trailer> trailers { get; set; } = default!;
        public List<string> genres { get; set; } = default!;
        public List<string> ratingDescriptors { get; set; } = default!;
        public List<object> tags { get; set; } = default!;
        public List<object> completeTags { get; set; } = default!;
        public List<MovieRoom> rooms { get; set; } = default!;
        public List<Session> sessionTypes { get; set; } = default!;
        public RottenTomatoe rottenTomatoe { get; set; } = default!;
    }

    public class RealDate
    {
        public DateTime? localDate { get; set; } = default!;
        public bool? isToday { get; set; } = default!;
        public string dayOfWeek { get; set; } = default!;
        public string dayAndMonth { get; set; } = default!;
        public string hour { get; set; } = default!;
        public string year { get; set; } = default!;
    }

    public class MovieRoom
    {
        public string name { get; set; } = default!;
        public object type { get; set; } = default!;
        public List<Session> sessions { get; set; } = default!;
    }

    public class IngressoMovie
    {
        public List<Movie> movies { get; set; } = default!;
        public string date { get; set; } = default!;
        public string dateFormatted { get; set; } = default!;
        public string dayOfWeek { get; set; } = default!;
        public bool isToday { get; set; } = default!;
    }

    public class RottenTomatoe
    {
        public string id { get; set; } = default!;
        public string criticsRating { get; set; } = default!;
        public string criticsScore { get; set; } = default!;
        public string audienceRating { get; set; } = default!;
        public string audienceScore { get; set; } = default!;
        public string originalUrl { get; set; } = default!;
    }

    public class Session
    {
        public string id { get; set; } = default!;
        public double price { get; set; } = default!;
        public string room { get; set; } = default!;
        public List<string> type { get; set; } = default!;
        public List<Type> types { get; set; } = default!;
        public Date date { get; set; } = default!;
        public RealDate realDate { get; set; } = default!;
        public string time { get; set; } = default!;
        public string defaultSector { get; set; } = default!;
        public object midnightMessage { get; set; } = default!;
        public string siteURL { get; set; } = default!;
        public object nationalSiteURL { get; set; } = default!;
        public bool hasSeatSelection { get; set; } = default!;
        public bool driveIn { get; set; } = default!;
        public bool streaming { get; set; } = default!;
        public int maxTicketsPerCar { get; set; } = default!;
        public bool enabled { get; set; } = default!;
        public string blockMessage { get; set; } = default!;
    }

    public class Trailer
    {
        public string type { get; set; } = default!;
        public string url { get; set; } = default!;
        public string embeddedUrl { get; set; } = default!;
    }

    public class Type
    {
        public int id { get; set; } = default!;
        public string name { get; set; } = default!;
        public string alias { get; set; } = default!;
        public bool display { get; set; } = default!;
    }


}
