using Microsoft.EntityFrameworkCore;
using TestShortUrl.Abstarcts;
using TestShortUrl.Data;
using TestShortUrl.Services;

namespace TestShortUrl.Extensions
{
    public static class ServiceExtensions
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 34))));
            return builder;
        }
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IWorker, WorkerService>();
            return builder;
        }

    }
}
