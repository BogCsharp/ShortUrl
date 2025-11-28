using System.Runtime.CompilerServices;
using TestShortUrl.Abstarcts;
using TestShortUrl.Data;
using TestShortUrl.Entities;
using TestShortUrl.Models;

namespace TestShortUrl.Services
{
    public class WorkerService : IWorker
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new();
        private const string Characters= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WorkerService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CreateShortUrlDTO> CreateShortUrlAsync(string oldUrl)
        {
            if (!IsValidUrl(oldUrl))
            {
                throw new ArgumentException("Неверный URL");
            }
            string shortUrl=GenerateShortUrl();
            var shortUrlEntity = new ShortUrl
            {
                NewUrl = shortUrl,
                OldUrl = oldUrl
            };
            _context.Urls.Add(shortUrlEntity);
            await _context.SaveChangesAsync();

            var baseUrl = GetBaseUrl();

            return new CreateShortUrlDTO
            {
                NewUrl = $"{baseUrl}/r/{shortUrl}",
                OldUrl = oldUrl
            };

           
        }

        
        private string GenerateShortUrl()
        {
            var chars = new char[6];
            for(int i=0; i<chars.Length; i++)
            {
                chars[i] = Characters[_random.Next(Characters.Length)];
            }
            return new string(chars);
        }
        private bool IsValidUrl(string url)
        {
            if(string.IsNullOrWhiteSpace(url)) return false;
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        private string GetBaseUrl()
        {
            var httpContext=_httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return string.Empty;
            }
            var request=httpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        public async Task<string> GetOriginalUrl(string url)
        {
            if(url == null) 
                throw new ArgumentException("Укажите ссылку");
            var shortUrl=await _context.Urls.FindAsync(url);
            if (shortUrl == null)
            {
                throw new ArgumentException("Ссылка не найдена");
            }
            return shortUrl.OldUrl;
        }
    }
}
