namespace api.Models.Exception
{
    public class NotUnderstandException : System.Exception
    {
        public string? Reason { get; set; } = default!;

        public NotUnderstandException(string? reason = null)
        {
            Reason = reason;
        }
    }
}
