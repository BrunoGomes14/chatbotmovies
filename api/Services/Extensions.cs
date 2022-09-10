using System.Text;

namespace api.Services
{
    public static class Extensions
    {
        public static string FromBase64(this string text)
        {
            byte[] data = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(data);
        }
    }
}