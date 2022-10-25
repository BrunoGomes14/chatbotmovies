namespace api.Models
{
    public class Date
    {
        public DateTime? localDate { get; set; }
        public bool? isToday { get; set; }
        public string dayOfWeek { get; set; }
        public string dayAndMonth { get; set; }
        public string hour { get; set; }
        public string year { get; set; }
    }

    public class MovieImage
    {
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Movie
    {
        public string id { get; set; }
        public string title { get; set; }
        public string originalTitle { get; set; }
        public string movieIdUrl { get; set; }
        public bool inPreSale { get; set; }
        public bool isReexhibition { get; set; }
        public string duration { get; set; }
        public string contentRating { get; set; }
        public string distributor { get; set; }
        public string urlKey { get; set; }
        public string siteURL { get; set; }
        public string nationalSiteURL { get; set; }
        public string siteURLByTheater { get; set; }
        public string nationalSiteURLByTheater { get; set; }
        public string boxOfficeId { get; set; }
        public string ancineId { get; set; }
        public List<MovieImage> images { get; set; }
        public List<Trailer> trailers { get; set; }
        public List<string> genres { get; set; }
        public List<string> ratingDescriptors { get; set; }
        public List<object> tags { get; set; }
        public List<object> completeTags { get; set; }
        public List<MovieRoom> rooms { get; set; }
        public List<Session> sessionTypes { get; set; }
        public RottenTomatoe rottenTomatoe { get; set; }
    }

    public class RealDate
    {
        public DateTime? localDate { get; set; }
        public bool? isToday { get; set; }
        public string dayOfWeek { get; set; }
        public string dayAndMonth { get; set; }
        public string hour { get; set; }
        public string year { get; set; }
    }

    public class MovieRoom
    {
        public string name { get; set; }
        public object type { get; set; }
        public List<Session> sessions { get; set; }
    }

    public class IngressoMovie
    {
        public List<Movie> movies { get; set; }
        public string date { get; set; }
        public string dateFormatted { get; set; }
        public string dayOfWeek { get; set; }
        public bool isToday { get; set; }
    }

    public class RottenTomatoe
    {
        public string id { get; set; }
        public string criticsRating { get; set; }
        public string criticsScore { get; set; }
        public string audienceRating { get; set; }
        public string audienceScore { get; set; }
        public string originalUrl { get; set; }
    }

    public class Session
    {
        public string id { get; set; }
        public double price { get; set; }
        public string room { get; set; }
        public List<string> type { get; set; }
        public List<Type> types { get; set; }
        public Date date { get; set; }
        public RealDate realDate { get; set; }
        public string time { get; set; }
        public string defaultSector { get; set; }
        public object midnightMessage { get; set; }
        public string siteURL { get; set; }
        public object nationalSiteURL { get; set; }
        public bool hasSeatSelection { get; set; }
        public bool driveIn { get; set; }
        public bool streaming { get; set; }
        public int maxTicketsPerCar { get; set; }
        public bool enabled { get; set; }
        public string blockMessage { get; set; }
    }

    public class Trailer
    {
        public string type { get; set; }
        public string url { get; set; }
        public string embeddedUrl { get; set; }
    }

    public class Type
    {
        public int id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public bool display { get; set; }
    }


}
