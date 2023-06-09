using System.Security.Cryptography;
using System.Text;

namespace Logitar.Demo.Core.Security;

internal static class SecurityHelper
{
  public static string GenerateSecret(int length = 256 / 8)
  {
    while (true)
    {
      /* In the ASCII table, there are 94 characters between 33 '!' and 126 '~' (126 - 33 + 1 = 94).
       * Since there are a total of 256 possibilities, by dividing per 94 and adding a 10% margin we
       * generate just a little more bytes than required, obtaining the factor 3. */
      byte[] bytes = RandomNumberGenerator.GetBytes(length * 3);

      List<byte> secret = new(capacity: length);
      for (int i = 0; i < bytes.Length; i++)
      {
        byte @byte = bytes[i];
        if (@byte >= 33 && @byte <= 126)
        {
          secret.Add(@byte);

          if (secret.Count == length)
          {
            return Encoding.ASCII.GetString(secret.ToArray());
          }
        }
      }
    }
  }
}
