namespace api.Models
{
    public class MovieToSend
    {
        public string Name { get; set; } = default!;
        public string StreamAvailable { get; set; } = default!;
        public string Overview { get; set; } = default!;
        public string UrlBackdrop { get; set; } = default!;
    }
}
