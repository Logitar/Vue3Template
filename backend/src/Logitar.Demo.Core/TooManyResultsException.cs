namespace Logitar.Demo.Core;

public class TooManyResultsException : Exception
{
  public TooManyResultsException() : base("Too many results have been found.")
  {
  }
}
