namespace api.Models
{
    public class MessageDecision
    {
        public string Messsage { get; set; } = default!;
        public PeopleStatus Status { get; set; } = default!;
        public Uri Media { get; set; } = default!;
    }
}
