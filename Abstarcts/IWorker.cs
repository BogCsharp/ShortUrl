using TestShortUrl.Entities;
using TestShortUrl.Models;

namespace TestShortUrl.Abstarcts
{
    public interface IWorker
    {
        Task<CreateShortUrlDTO> CreateShortUrlAsync(string oldUrl);
        Task <string> GetOriginalUrl(string url);

    }
}
