using GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Demo.Schema.Extensions;

internal static class ResolveFieldContextExtensions
{
  public static TService GetRequiredService<TService, TSource>(this IResolveFieldContext<TSource> context) where TService : notnull
  {
    if (context.RequestServices == null)
    {
      throw new InvalidOperationException($"The {nameof(context.RequestServices)} is required.");
    }

    return context.RequestServices.GetRequiredService<TService>();
  }
}
