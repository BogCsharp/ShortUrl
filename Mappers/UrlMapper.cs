using TestShortUrl.Entities;
using TestShortUrl.Models;

namespace TestShortUrl.Mappers
{
    public static class UrlMapper
    {
        public static CreateShortUrlDTO ToDto(this ShortUrl entity)
        {
            return new CreateShortUrlDTO
            {
                NewUrl=entity.NewUrl,
                OldUrl = entity.OldUrl
            };
        }
        public static CreateUrlResponse ToCreateUrlResponse(this  ShortUrl entity)
        {
            return new CreateUrlResponse
            {
                NewUrl = entity.NewUrl
            };
        }
    }
}
