namespace api.Models
{
    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
        public string uf { get; set; }
        public string state { get; set; }
        public string urlKey { get; set; }
        public string timeZone { get; set; }
    }

    public class IngresssoStates
    {
        public string name { get; set; }
        public string uf { get; set; }
        public List<City> cities { get; set; }
    }
}
