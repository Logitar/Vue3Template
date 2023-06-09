using Logitar.Demo.Core;
using Logitar.Demo.Core.Actors;
using Logitar.Demo.Web.Extensions;
using Logitar.EventSourcing;
using Logitar.Portal.Contracts.Users;

namespace Logitar.Demo.Web;

internal class HttpApplicationContext : IApplicationContext
{
  private static readonly Actor _system = new();

  private readonly IHttpContextAccessor _httpContextAccessor;

  public HttpApplicationContext(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public Actor Actor
  {
    get
    {
      if (_httpContextAccessor.HttpContext != null)
      {
        User? user = _httpContextAccessor.HttpContext.GetUser();
        if (user != null)
        {
          return new Actor
          {
            Id = user.Id,
            Type = ActorType.User,
            DisplayName = user.FullName ?? user.Username,
            EmailAddress = user.Email?.Address,
            Picture = user.Picture
          };
        }
      }

      return _system;
    }
  }
  public AggregateId ActorId => new(Actor.Id);
}
