using GraphQL.Types;
using Logitar.Demo.Core;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Actors;

internal class QueryOperatorGraphType : EnumerationGraphType<QueryOperator>
{
  public QueryOperatorGraphType()
  {
    Name = nameof(QueryOperator);
    Description = "Defines the available operators that can be used to query data.";

    this.AddValue(QueryOperator.And, "All specified values must match for a result to be returned.");
    this.AddValue(QueryOperator.Or, "Only one of the specified values must match for a result to be returned.");
  }
}
