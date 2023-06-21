using GraphQL;
using GraphQL.Types;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Realms;

internal static class RealmQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<RealmGraphType>("realm")
      .Description("Retrieves a realm.")
      .Arguments(new QueryArguments(
        new QueryArgument<IdGraphType>() { Name = "id", Description = "The unique identifier of the realm." },
        new QueryArgument<StringGraphType>() { Name = "uniqueName", Description = "The unique name of the realm." }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().GetAsync(
        context.GetArgument<Guid?>("id"),
        context.GetArgument<string?>("uniqueName"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<SearchResultsGraphType<Realm, RealmGraphType>>>("realms")
      .Description("Searches a list of realms.")
      .Arguments(new QueryArguments(
        new QueryArgument<NonNullGraphType<RealmSearchParametersGraphType>>() { Name = "parameters", Description = "The parameters of the search." }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().SearchAsync(
        context.GetArgument<RealmSearchParameters>("parameters"),
        context.CancellationToken
      ));
  }
}
