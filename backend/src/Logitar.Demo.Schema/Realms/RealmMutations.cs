using GraphQL;
using GraphQL.Types;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Realms;

internal static class RealmMutations
{
  public static void Register(RootMutation root)
  {
    root.Field<NonNullGraphType<RealmGraphType>>("createRealm")
      .Description("Creates a new realm.")
      .Arguments(new QueryArguments(
        new QueryArgument<NonNullGraphType<CreateRealmGraphType>>() { Name = "input", Description = "The data used to create a new realm." }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().CreateAsync(
        context.GetArgument<CreateRealmInput>("input"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<RealmGraphType>>("deleteRealm")
      .Description("Deletes an existing realm.")
      .Arguments(new QueryArguments(
        new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "id", Description = "The unique identifier of the realm." }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().DeleteAsync(
        context.GetArgument<Guid>("id"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<RealmGraphType>>("updateRealm")
      .Description("Updates an existing a realm.")
      .Arguments(new QueryArguments(
        new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "id", Description = "The unique identifier of the realm." },
        new QueryArgument<NonNullGraphType<UpdateRealmGraphType>>() { Name = "input", Description = "The data used to update the realm." }
      ))
      .ResolveAsync(async context => await context.GetRequiredService<IRealmService, object?>().UpdateAsync(
        context.GetArgument<Guid>("id"),
        context.GetArgument<UpdateRealmInput>("input"),
        context.CancellationToken
      ));
  }
}
