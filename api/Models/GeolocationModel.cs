namespace api.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressComponent
    {
        public string long_name { get; set; } = default!;
        public string short_name { get; set; } = default!;
        public List<string> types { get; set; } = default!;
    }

    public class Bounds
    {
        public Northeast northeast { get; set; } = default!;
        public Southwest southwest { get; set; } = default!;
    }

    public class Geometry
    {
        public Bounds bounds { get; set; } = default!;
        public Location location { get; set; } = default!;
        public string location_type { get; set; } = default!;
        public Viewport viewport { get; set; } = default!;
    }

    public class Location
    {
        public decimal lat { get; set; } = default!;
        public decimal lng { get; set; } = default!;
    }

    public class Northeast
    {
        public double lat { get; set; } = default!;
        public double lng { get; set; } = default!;
    }

    public class PlusCode
    {
        public string compound_code { get; set; } = default!;
        public string global_code { get; set; } = default!;
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; } = default!;
        public string formatted_address { get; set; } = default!;
        public Geometry geometry { get; set; } = default!;
        public string place_id { get; set; } = default!;
        public List<string> types { get; set; } = default!;
        public PlusCode plus_code { get; set; } = default!;
    }

    public class GeolocationModel
    {
        public PlusCode plus_code { get; set; } = default!;
        public List<Result> results { get; set; } = default!;
        public string status { get; set; } = default!;
    }

    public class Southwest
    {
        public double lat { get; set; } = default!;
        public double lng { get; set; } = default!;
    }

    public class Viewport
    {
        public Northeast northeast { get; set; } = default!;
        public Southwest southwest { get; set; } = default!;
    }


}
