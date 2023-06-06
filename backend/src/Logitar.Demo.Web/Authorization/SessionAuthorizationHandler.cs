using Logitar.Demo.Web.Extensions;
using Logitar.Portal.Contracts.Users;
using Microsoft.AspNetCore.Authorization;

namespace Logitar.Demo.Web.Authorization;

internal class SessionAuthorizationHandler : AuthorizationHandler<SessionAuthorizationRequirement>
{
  private readonly string _realm;

  private readonly IHttpContextAccessor _httpContextAccessor;

  public SessionAuthorizationHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
  {
    _realm = configuration.GetSection("Portal").GetValue<string>("Realm")
      ?? throw new InvalidOperationException("The configuration key 'Portal:Realm' has not been set.");

    _httpContextAccessor = httpContextAccessor;
  }

  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SessionAuthorizationRequirement requirement)
  {
    if (_httpContextAccessor.HttpContext != null)
    {
      User? user = _httpContextAccessor.HttpContext.GetUser();
      if (user == null)
      {
        context.Fail(new AuthorizationFailureReason(this, "The User is required."));
      }
      else if (user.Realm?.UniqueName != _realm)
      {
        context.Fail(new AuthorizationFailureReason(this, $"The User should belong to the realm '{_realm}'."));
      }
      else if (_httpContextAccessor.HttpContext.GetSession() == null)
      {
        context.Fail(new AuthorizationFailureReason(this, "The Session is required."));
      }
      else
      {
        context.Succeed(requirement);
      }
    }

    return Task.CompletedTask;
  }
}
