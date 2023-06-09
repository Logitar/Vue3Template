using Logitar.EventSourcing;
using System.Text;

namespace Logitar.Demo.Core;

public class AggregateNotFoundException : Exception
{
  public AggregateNotFoundException(Type type, string id, string? paramName) : base(GetMessage(type, id, paramName))
  {
    Data["Type"] = type.GetName();
    Data[nameof(Id)] = id;

    if (paramName != null)
    {
      Data[nameof(ParamName)] = paramName;
    }
  }

  public string Id => (string)Data[nameof(Id)]!;
  public string? ParamName => (string?)Data[nameof(ParamName)];

  private static string GetMessage(Type type, string id, string? paramName)
  {
    StringBuilder message = new();

    message.AppendLine("The specified aggregate could not be found.");
    message.Append("Type: ").Append(type.GetName()).AppendLine();
    message.Append("Id: ").Append(id).AppendLine();

    if (paramName != null)
    {
      message.Append("ParamName: ").Append(paramName).AppendLine();
    }

    return message.ToString();
  }
}

public class AggregateNotFoundException<T> : AggregateNotFoundException
{
  public AggregateNotFoundException(Guid id, string? paramName = null) : this(id.ToString(), paramName)
  {
  }
  public AggregateNotFoundException(string id, string? paramName = null) : base(typeof(T), id, paramName)
  {
  }
}
