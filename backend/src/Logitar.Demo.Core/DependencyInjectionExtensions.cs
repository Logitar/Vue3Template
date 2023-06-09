using Logitar.Demo.Core.Realms;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logitar.Demo.Core;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarDemoCore(this IServiceCollection services)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services
      .AddDomainServices()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
  }

  private static IServiceCollection AddDomainServices(this IServiceCollection services)
  {
    return services.AddTransient<IRealmService, RealmService>();
  }
}
