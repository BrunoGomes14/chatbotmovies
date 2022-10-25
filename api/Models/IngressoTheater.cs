namespace api.Models
{
    public class Document
    {
        public string name { get; set; }
        public string number { get; set; }
        public DateTime expiration { get; set; }
    }

    public class Functionalities
    {
        public bool operationPolicyEnabled { get; set; }
    }

    public class Geolocation
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string urlKey { get; set; }
        public string name { get; set; }
        public string siteURL { get; set; }
        public string nationalSiteURL { get; set; }
        public string cnpj { get; set; }
        public string districtAuthorization { get; set; }
        public string address { get; set; }
        public string addressComplement { get; set; }
        public string number { get; set; }
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string state { get; set; }
        public string uf { get; set; }
        public string neighborhood { get; set; }
        public Properties properties { get; set; }
        public Functionalities functionalities { get; set; }
        public List<string> telephones { get; set; }
        public Geolocation geolocation { get; set; }
        public List<string> deliveryType { get; set; }
        public string corporation { get; set; }
        public string corporationId { get; set; }
        public int corporationPriority { get; set; }
        public string corporationAvatarBackground { get; set; }
        public List<Room> rooms { get; set; }
        public int totalRooms { get; set; }
        public bool enabled { get; set; }
        public string blockMessage { get; set; }
        public object partnershipType { get; set; }
        public List<object> operationPolicies { get; set; }
        public decimal Distance { get; set; }
    }

    public class Properties
    {
        public bool hasBomboniere { get; set; }
        public bool hasContactlessWithdrawal { get; set; }
        public bool hasSession { get; set; }
        public bool hasSeatDistancePolicy { get; set; }
        public bool hasSeatDistancePolicyArena { get; set; }
    }

    public class Room
    {
        public string id { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public int capacity { get; set; }
        public List<Document> documents { get; set; }
    }

    public class IngressoTheater
    {
        public List<Item> items { get; set; }
        public int count { get; set; }
    }
}
