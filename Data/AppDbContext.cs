using Microsoft.EntityFrameworkCore;
using TestShortUrl.Entities;

namespace TestShortUrl.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
        public DbSet<ShortUrl> Urls {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl>()
                .HasKey(s => s.NewUrl);//Установка NewUrl как первичный ключ
            modelBuilder.Entity<ShortUrl>()
            .Property(s => s.NewUrl)
            .HasMaxLength(6); // Ограничиваем длину 
        }
    }
}
