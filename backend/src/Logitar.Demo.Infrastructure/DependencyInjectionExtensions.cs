using Logitar.Demo.Core.Realms;
using Logitar.Demo.Infrastructure.Actors;
using Logitar.Demo.Infrastructure.Queriers;
using Logitar.Demo.Infrastructure.Repositories;
using Logitar.EventSourcing;
using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logitar.Demo.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLogitarDemoInfrastructure(this IServiceCollection services, string connectionString)
  {
    Assembly assembly = typeof(DependencyInjectionExtensions).Assembly;

    return services.AddAutoMapper(assembly)
      .AddDbContext<DemoContext>(builder => builder.UseNpgsql(connectionString))
      .AddEventSourcingWithEntityFrameworkCorePostgreSQL(connectionString)
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
      .AddQueriers()
      .AddRepositories()
      .AddScoped<IActorService, ActorService>()
      .AddScoped<IEventBus, EventBus>();
  }

  private static IServiceCollection AddQueriers(this IServiceCollection services)
  {
    return services.AddScoped<IRealmQuerier, RealmQuerier>();
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    return services.AddScoped<IRealmRepository, RealmRepository>();
  }
}
