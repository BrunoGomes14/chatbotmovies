namespace api.Models
{
    public class ContentOption
    {
        public ContentOption()
        {
            VerbsOptions = new List<string>();
        }

        public List<string> VerbsOptions { get; set; } = default!;
        public int NumberOption { get; set; } = default!;
        public List<string>? Substantive { get; set; } = default!;
    }
}