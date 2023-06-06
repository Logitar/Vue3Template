using Logitar.Demo.Web.Constants;
using Logitar.Demo.Web.Extensions;
using Logitar.Portal.Contracts.Errors;
using Logitar.Portal.Contracts.Sessions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Logitar.Demo.Web.Authentication;

internal class SessionAuthenticationHandler : AuthenticationHandler<SessionAuthenticationOptions>
{
  private readonly ISessionService _sessionService;

  public SessionAuthenticationHandler(ISessionService sessionService,
    IOptionsMonitor<SessionAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock) : base(options, logger, encoder, clock)
  {
    _sessionService = sessionService;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    Guid? sessionId = Context.GetSessionId();
    if (sessionId.HasValue)
    {
      AuthenticateResult? failure = null;

      Session? session = null;
      try
      {
        session = await _sessionService.GetAsync(sessionId.Value);
      }
      catch (ErrorException exception)
      {
        ErrorData? statusCode = exception.Error.Data.SingleOrDefault(d => d.Key == "StatusCode");
        if (statusCode?.Value != StatusCodes.Status404NotFound.ToString())
        {
          throw;
        }
      }

      if (session == null)
      {
        failure = AuthenticateResult.Fail($"The session 'Id={sessionId}' could not be found.");
      }
      else if (!session.IsActive)
      {
        failure = AuthenticateResult.Fail($"The session 'Id={session.Id}' has ended.");
      }
      else if (session.User.IsDisabled)
      {
        failure = AuthenticateResult.Fail($"The User is disabled for session 'Id={session.Id}'.");
      }

      if (failure != null)
      {
        Context.SignOut();

        return failure;
      }

      Context.SetSession(session);
      Context.SetUser(session!.User);

      ClaimsPrincipal principal = new(new ClaimsIdentity(Schemes.Session));
      AuthenticationTicket ticket = new(principal, Schemes.Session);

      return AuthenticateResult.Success(ticket);
    }

    return AuthenticateResult.NoResult();
  }
}
