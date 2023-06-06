using Logitar.Demo.Web.Constants;
using Logitar.Demo.Web.Extensions;
using Logitar.Portal.Contracts.Sessions;

namespace Logitar.Demo.Web.Middlewares;

internal class RefreshSession
{
  private readonly RequestDelegate _next;

  public RefreshSession(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context, ISessionService sessionService)
  {
    if (!context.IsSignedIn())
    {
      HttpRequest request = context.Request;

      if (request.Cookies.TryGetValue(Cookies.RefreshToken, out string? refreshToken))
      {
        try
        {
          RefreshInput input = new()
          {
            RefreshToken = refreshToken,
            IpAddress = context.GetClientIpAddress(),
            AdditionalInformation = context.GetAdditionalInformation()
          };
          Session session = await sessionService.RefreshAsync(input);
          context.SignIn(session);
        }
        catch (Exception)
        {
          context.SignOut();
        }
      }
    }

    await _next(context);
  }
}
