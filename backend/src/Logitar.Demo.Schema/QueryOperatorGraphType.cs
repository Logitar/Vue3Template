using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema.Actors;

internal class QueryOperatorGraphType : EnumerationGraphType<QueryOperator>
{
  public QueryOperatorGraphType()
  {
    Name = nameof(QueryOperator);
    Description = "...";

    Add(new EnumValueDefinition(nameof(QueryOperator.And), QueryOperator.And)
    {
      Description = "..."
    });
    Add(new EnumValueDefinition(nameof(QueryOperator.Or), QueryOperator.Or)
    {
      Description = "..."
    });
  }
}
