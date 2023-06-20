using GraphQL.Types;

namespace Logitar.Demo.Schema.Extensions;

internal static class GraphTypeExtensions
{
  public static void AddValue<T>(this EnumerationGraphType<T> type, T value, string? description = null) where T : Enum
  {
    type.Add(new EnumValueDefinition(value.ToString(), value)
    {
      Description = description
    });
  }
}
