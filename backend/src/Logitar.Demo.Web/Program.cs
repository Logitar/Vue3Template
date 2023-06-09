
using Logitar.Demo.Infrastructure;
using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Demo.Web;

public class Program
{
  public static async Task Main(string[] args)
  {
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    Startup startup = new(builder.Configuration);
    startup.ConfigureServices(builder.Services);

    WebApplication application = builder.Build();

    startup.Configure(application);

    if (application.Configuration.GetValue<bool>("EnableMigrations"))
    {
      using IServiceScope scope = application.Services.CreateScope();

      using EventContext eventContext = scope.ServiceProvider.GetRequiredService<EventContext>();
      await eventContext.Database.MigrateAsync();

      using DemoContext demoContext = scope.ServiceProvider.GetRequiredService<DemoContext>();
      await demoContext.Database.MigrateAsync();
    }

    application.Run();
  }
}
