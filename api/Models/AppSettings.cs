namespace api.Models
{
    public class AppSettings
    {
        public TwilioConfig? TwilioConfig { get; set; }
        public string GoogleKey { get; set; } = "";
        public string TmdbKey { get; set; } = "";
    }
}