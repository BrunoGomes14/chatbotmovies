namespace api.Models
{
    public class ContentOption
    {
        public ContentOption()
        {
            VerbsOptions = new List<string>();
        }

        public List<string> VerbsOptions { get; set; }
        public int NumberOption { get; set; }
        public List<string>? Substantive { get; set; }
    }
}