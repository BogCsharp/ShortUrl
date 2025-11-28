using Microsoft.EntityFrameworkCore;
using TestShortUrl.Data;
using TestShortUrl.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Short URL API",
        Version = "v1"
    });
});
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder
    .AddData()
    .AddServices();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("Создание БД");
        Thread.Sleep(10000); 

        Console.WriteLine("Применение миграций");
        dbContext.Database.Migrate(); 
        Console.WriteLine("Миграции накатились успешно");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        Console.WriteLine($"Полный стек ошибки: {ex}");
    }
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Short URL API V1");
    c.RoutePrefix = "swagger";
});


app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
