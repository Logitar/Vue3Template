using GraphQL;
using GraphQL.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Demo.Schema;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarDemoSchema(this IServiceCollection services, IConfiguration configuration)
  {
    GraphQLSettings settings = configuration.GetSection("GraphQL").Get<GraphQLSettings>() ?? new();

    return services.AddLogitarDemoSchema(settings);
  }

  public static IServiceCollection AddLogitarDemoSchema(this IServiceCollection services, GraphQLSettings settings)
  {
    return services.AddGraphQL(builder => builder
      .AddAuthorizationRule()
      .AddErrorInfoProvider(new ErrorInfoProvider(options => options.ExposeExceptionDetails = settings.ExposeExceptionDetails))
      .AddGraphTypes(typeof(DemoSchema).Assembly)
      .AddSchema<DemoSchema>()
      .AddSystemTextJson()
      .ConfigureExecutionOptions(options => options.EnableMetrics = settings.EnableMetrics));
  }
}
