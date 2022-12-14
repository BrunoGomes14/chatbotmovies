namespace api.Models;

public class WhatsappMessage
{
    public string ProfileName { get; set; } = "";
    public string WaId { get; set; } = "";
    public string ButtonText { get; set; } = "";
    public string MessageSid { get; set; } = "";
    public string AccountSid { get; set; } = "";
    public string From { get; set; } = "";
    public string? Body { get; set; } = default!;   
    public string? Latitude { get; set; } = default!;
    public string? Longitude { get; set; } = default!;  
    public int NumMedia { get; set; } = default!;
}
