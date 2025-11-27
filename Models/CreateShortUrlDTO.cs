namespace TestShortUrl.Models
{
    public class CreateShortUrlDTO
    {
        public string NewUrl { get; set; }=string.Empty;
        public string OldUrl { get; set; }=string.Empty;
    }
}
