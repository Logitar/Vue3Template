using Logitar.Demo.Web.Extensions;
using Logitar.Demo.Web.Middlewares;
using Logitar.Portal.Client;
using System.Text.Json.Serialization;

namespace Logitar.Demo.Web;

internal class Startup : StartupBase
{
  private readonly CorsSettings? _corsSettings;
  private readonly bool _enableOpenApi;

  private readonly IConfiguration _configuration;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;

    _corsSettings = configuration.GetSection("Cors").Get<CorsSettings>();
    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services.AddControllers()
      .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    services.AddApplicationInsightsTelemetry();
    services.AddHealthChecks()/*.AddDbContextCheck<EventContext>()*/; // TODO(fpion): implement

    if (_corsSettings != null)
    {
      services.AddCors(_corsSettings);
    }

    if (_enableOpenApi)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    services
      .AddSession(options =>
      {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      })
      .AddDistributedMemoryCache();

    services.AddLogitarPortalClient(_configuration);
  }

  public override void Configure(IApplicationBuilder builder)
  {
    if (_enableOpenApi)
    {
      builder.UseSwagger();
      builder.UseSwaggerUI();
    }

    builder.UseHttpsRedirection();

    if (_corsSettings != null)
    {
      builder.UseCors();
    }

    builder.UseSession();
    builder.UseMiddleware<RefreshSession>();

    if (builder is WebApplication application)
    {
      application.MapControllers();
      application.MapHealthChecks("/health");
    }
  }
}
