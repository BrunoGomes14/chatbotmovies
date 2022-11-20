namespace api.Models
{
    public class AppSettings
    {
        public TwilioConfig? TwilioConfig { get; set; } = default;
        public string GoogleKey { get; set; } = "";
        public string TmdbKey { get; set; } = "";
    }
}