
namespace Lab.OutputCache
{
  public class Program
  {
    public static void Main(string[] args)
    {

      var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)        
        .Build();

      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddOutputCache(o =>
      {
        var minutesToLive = Convert.ToInt32(configuration["OutputCache:MinutesToLive"]);

        o.AddBasePolicy(p => p
          .Expire(TimeSpan.FromMinutes(minutesToLive)));

      });






      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.UseOutputCache();

      app.MapControllers();

      app.Run();
    }
  }
}
