using System.Text;

namespace Logitar.Demo.Core;

public class InvalidLocaleException : Exception, IPropertyFailure
{
  public InvalidLocaleException(string value, string paramName, Exception? innerException = null)
    : base(GetMessage(value, paramName), innerException)
  {
    Data[nameof(Value)] = value;
    Data[nameof(ParamName)] = paramName;
  }

  public string ParamName => (string)Data[nameof(ParamName)]!;
  public string Value => (string)Data[nameof(Value)]!;

  private static string GetMessage(string value, string paramName)
  {
    StringBuilder message = new();

    message.Append("The locale '").Append(value).AppendLine("' is not valid.");
    message.Append("ParamName: ").Append(paramName).AppendLine();

    return message.ToString();
  }
}
