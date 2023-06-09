using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Infrastructure.Entities;

internal class ActorEntity
{
  public int ActorId { get; private set; }

  public Guid Id { get; private set; }
  public ActorType Type { get; private set; }
  public bool IsDeleted { get; private set; }

  public string DisplayName { get; private set; } = ActorType.System.ToString();
  public string? EmailAddress { get; private set; }
  public string? Picture { get; private set; }
}
