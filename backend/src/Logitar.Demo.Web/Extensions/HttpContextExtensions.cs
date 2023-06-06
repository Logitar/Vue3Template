using Logitar.Demo.Web.Constants;
using Logitar.Portal.Contracts.Sessions;
using Logitar.Portal.Contracts.Users;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Logitar.Demo.Web.Extensions;

internal static class HttpContextExtensions
{
  private const string SessionIdKey = "SessionId";
  private const string SessionKey = "Session";
  private const string UserKey = "User";

  public static string? GetClientIpAddress(this HttpContext context)
  {
    string? ipAddress = null;

    if (context.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues xForwardedFor))
    {
      ipAddress = xForwardedFor.Single()?.Split(':').First();
    }
    ipAddress ??= context.Connection.RemoteIpAddress?.ToString();

    return ipAddress;
  }
  public static string GetAdditionalInformation(this HttpContext context)
  {
    return JsonSerializer.Serialize(context.Request.Headers);
  }

  public static Session? GetSession(this HttpContext context) => context.GetItem<Session>(SessionKey);
  public static User? GetUser(this HttpContext context) => context.GetItem<User>(UserKey);
  private static T? GetItem<T>(this HttpContext context, object key)
  {
    return context.Items.TryGetValue(key, out object? value) ? (T?)value : default;
  }

  public static void SetSession(this HttpContext context, Session? session) => context.SetItem(SessionKey, session);
  public static void SetUser(this HttpContext context, User? user) => context.SetItem(UserKey, user);
  private static void SetItem<T>(this HttpContext context, object key, T? value)
  {
    if (value == null)
    {
      context.Items.Remove(key);
    }
    else
    {
      context.Items[key] = value;
    }
  }

  public static Guid? GetSessionId(this HttpContext context)
  {
    byte[]? bytes = context.Session.Get(SessionIdKey);

    return bytes == null ? null : new(bytes);
  }
  public static bool IsSignedIn(this HttpContext context) => context.GetSessionId().HasValue;
  public static void SignIn(this HttpContext context, Session session)
  {
    context.Session.Set(SessionIdKey, session.Id.ToByteArray());

    if (session.RefreshToken != null)
    {
      context.Response.Cookies.Append(Cookies.RefreshToken, session.RefreshToken, Cookies.RefreshTokenOptions);
    }
  }
  public static void SignOut(this HttpContext context)
  {
    context.Session.Clear();

    context.Response.Cookies.Delete(Cookies.RefreshToken);
  }
}
