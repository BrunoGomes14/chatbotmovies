namespace api.Models
{
    public class City
    {
        public string id { get; set; } = string.Empty;
        public string name { get; set; } = default!;
        public string uf { get; set; } = default!;
        public string state { get; set; } = default!;
        public string urlKey { get; set; } = default!;
        public string timeZone { get; set; } = default!;
    }

    public class IngresssoStates
    {
        public string name { get; set; } = default!;
        public string uf { get; set; } = default!;
        public List<City> cities { get; set; } = new()!;
    }
}
