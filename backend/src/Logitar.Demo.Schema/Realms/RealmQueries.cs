using GraphQL;
using GraphQL.Types;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Realms;

internal static class RealmQueries
{
  internal static void Register(RootQuery root)
  {
    root.Field<NonNullGraphType<SearchResultsGraphType<Realm, RealmGraphType>>>("realms")
      .Description("...")
      .Arguments(new QueryArguments(
        new QueryArgument<NonNullGraphType<RealmSearchParametersGraphType>>() { Name = "parameters" }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().SearchAsync(
        context.GetArgument<RealmSearchParameters>("parameters"),
        context.CancellationToken
      ));
  }
}
