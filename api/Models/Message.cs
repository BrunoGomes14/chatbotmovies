namespace api.Models
{
    public class Message
    {
        public string? Content { get; set; } = "";
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public decimal Latitude { get; set; } = default!;
        public decimal Longitude { get; set; } = default!;
    }
}