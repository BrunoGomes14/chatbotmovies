namespace api.Models
{
    public class AppSettings
    {
        public TwilioConfig? TwilioConfig { get; set; } = default;
        public string TelegramKey { get; set; } = string.Empty;
        public string GoogleKey { get; set; } = string.Empty;
        public string TmdbKey { get; set; } = string.Empty;
        public string MysqlConn { get; set; } = string.Empty;
    }
}