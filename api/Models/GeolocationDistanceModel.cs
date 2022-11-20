namespace api.Models
{
    public class Distance
    {
        public string text { get; set; } = string.Empty;
        public int value { get; set; } = default!;
    }

    public class Duration
    {
        public string text { get; set; } = string.Empty;
        public int value { get; set; } = default!;
    }

    public class Element
    {
        public Distance distance { get; set; } = default!;
        public Duration duration { get; set; } = default!; 
        public string status { get; set; } = string.Empty;
    }

    public class GeolocationDistanceModel
    {
        public List<string> destination_addresses { get; set; } = default!;
        public List<string> origin_addresses { get; set; } = default!;
        public List<Row> rows { get; set; } = default!;
        public string status { get; set; } = default!;
    }

    public class Row
    {
        public List<Element> elements { get; set; } = default!;
    }


}
