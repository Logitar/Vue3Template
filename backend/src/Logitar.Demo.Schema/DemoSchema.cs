using Microsoft.Extensions.DependencyInjection;

namespace Logitar.Demo.Schema;

public class DemoSchema : GraphQL.Types.Schema
{
  public DemoSchema(IServiceProvider serviceProvider) : base(serviceProvider)
  {
    Query = serviceProvider.GetRequiredService<RootQuery>();
  }
}
