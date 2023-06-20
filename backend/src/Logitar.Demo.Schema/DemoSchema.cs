using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Demo.Schema;

public class DemoSchema : GraphQL.Types.Schema
{
  public DemoSchema(IServiceProvider serviceProvider) : base(serviceProvider)
  {
    Mutation = serviceProvider.GetRequiredService<RootMutation>();
    Query = serviceProvider.GetRequiredService<RootQuery>();
  }
}
