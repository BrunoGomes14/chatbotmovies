namespace api.Models
{
    public class Document
    {
        public string name { get; set; } = default!;
        public string number { get; set; } = default!;
        public DateTime expiration { get; set; } = default!;
    }

    public class Functionalities
    {
        public bool operationPolicyEnabled { get; set; } = default!;
    }

    public class Geolocation
    {
        public decimal lat { get; set; } = default!;
        public decimal lng { get; set; } = default!;
    }

    public class Image
    {
        public string url { get; set; } = default!;
        public string type { get; set; } = default!;
    }

    public class Item
    {
        public string id { get; set; } = default!;
        public List<Image> images { get; set; } = default!;
        public string urlKey { get; set; } = default!;
        public string name { get; set; } = default!;
        public string siteURL { get; set; } = default!;
        public string nationalSiteURL { get; set; } = default!;
        public string cnpj { get; set; } = default!;
        public string districtAuthorization { get; set; } = default!;
        public string address { get; set; } = default!;
        public string addressComplement { get; set; } = default!;
        public string number { get; set; } = default!;
        public string cityId { get; set; } = default!;
        public string cityName { get; set; } = default!;
        public string state { get; set; } = default!;
        public string uf { get; set; } = default!;
        public string neighborhood { get; set; } = default!;
        public Properties properties { get; set; } = default!;
        public Functionalities functionalities { get; set; } = default!;
        public List<string> telephones { get; set; } = default!;
        public Geolocation geolocation { get; set; } = default!;
        public List<string> deliveryType { get; set; } = default!;
        public string corporation { get; set; } = default!;
        public string corporationId { get; set; } = default!;
        public int corporationPriority { get; set; } = default!;
        public string corporationAvatarBackground { get; set; } = default!;
        public List<Room> rooms { get; set; } = default!;
        public int totalRooms { get; set; } = default!;
        public bool enabled { get; set; } = default!;
        public string blockMessage { get; set; } = default!;
        public object partnershipType { get; set; } = default!;
        public List<object> operationPolicies { get; set; } = default!;
        public decimal Distance { get; set; } = default!;
    }

    public class Properties
    {
        public bool hasBomboniere { get; set; } = default!;
        public bool hasContactlessWithdrawal { get; set; } = default!;
        public bool hasSession { get; set; } = default!;
        public bool hasSeatDistancePolicy { get; set; } = default!;
        public bool hasSeatDistancePolicyArena { get; set; } = default!;
    }

    public class Room
    {
        public string id { get; set; } = default!;
        public string name { get; set; } = default!;
        public string fullName { get; set; } = default!;
        public int capacity { get; set; } = default!;
        public List<Document> documents { get; set; } = default!;
    }

    public class IngressoTheater
    {
        public List<Item> items { get; set; } = default!;
        public int count { get; set; } = default!;
    }
}
