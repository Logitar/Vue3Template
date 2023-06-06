﻿using Logitar.Demo.Web.Constants;
using Logitar.Portal.Contracts.Sessions;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Logitar.Demo.Web.Extensions;

internal static class HttpContextExtensions
{
  private const string SessionIdKey = "SessionId";

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

  public static void SignIn(this HttpContext context, Session session)
  {
    context.Session.Set(SessionIdKey, session.Id.ToByteArray());

    if (session.RefreshToken != null)
    {
      context.Response.Cookies.Append(Cookies.RefreshToken, session.RefreshToken, Cookies.RefreshTokenOptions);
    }
  }
}
