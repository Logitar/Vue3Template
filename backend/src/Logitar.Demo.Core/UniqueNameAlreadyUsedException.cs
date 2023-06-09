using System.Text;

namespace Logitar.Demo.Core;

public class UniqueNameAlreadyUsedException : Exception, IPropertyFailure
{
  public UniqueNameAlreadyUsedException(string value, string paramName)
    : base(GetMessage(value, paramName))
  {
    Data[nameof(Value)] = value;
    Data[nameof(ParamName)] = paramName;
  }

  public string ParamName => (string)Data[nameof(ParamName)]!;
  public string Value => (string)Data[nameof(Value)]!;

  private static string GetMessage(string value, string paramName)
  {
    StringBuilder message = new();

    message.Append("The specified unique name '").Append(value).AppendLine("' is already used.");
    message.Append("ParamName: ").Append(paramName).AppendLine();

    return message.ToString();
  }
}
