using Logitar.Demo.Web.Authentication;
using Logitar.Demo.Web.Authorization;
using Logitar.Demo.Web.Extensions;
using Logitar.Demo.Web.Middlewares;
using Logitar.Portal.Client;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

using DemoSchemes = Logitar.Demo.Web.Constants.Schemes;

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

    if (_enableOpenApi)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    if (_corsSettings != null)
    {
      services.AddCors(_corsSettings);
    }

    services.AddAuthentication()
      .AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(DemoSchemes.Session, options => { });

    services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder(DemoSchemes.Session)
      .RequireAuthenticatedUser()
      .AddRequirements(new SessionAuthorizationRequirement())
      .Build());
    services.AddSingleton<IAuthorizationHandler, SessionAuthorizationHandler>();

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
    builder.UseAuthentication();
    builder.UseAuthorization();

    if (builder is WebApplication application)
    {
      application.MapControllers();
      application.MapHealthChecks("/health");
    }
  }
}
