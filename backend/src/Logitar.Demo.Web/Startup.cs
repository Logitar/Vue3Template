using Logitar.Portal.Client;
using System.Text.Json.Serialization;

namespace Logitar.Demo.Web;

internal class Startup : StartupBase
{
  private readonly bool _enableOpenApi;

  private readonly IConfiguration _configuration;

  public Startup(IConfiguration configuration)
  {
    _configuration = configuration;

    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services.AddControllers()
      .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    if (_enableOpenApi)
    {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    services.AddCors(options => options.AddDefaultPolicy(policy => policy
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader())); // TODO(fpion): configure CORS

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
    builder.UseCors();

    if (builder is WebApplication application)
    {
      application.MapControllers();
    }
  }
}
