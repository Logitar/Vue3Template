using Logitar.EventSourcing;

namespace Logitar.Demo.Core;

public interface IApplicationContext
{
  AggregateId ActorId { get; }
}
