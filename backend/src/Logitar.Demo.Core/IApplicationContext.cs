using Logitar.Demo.Core.Actors;
using Logitar.EventSourcing;

namespace Logitar.Demo.Core;

public interface IApplicationContext
{
  Actor Actor { get; }
  AggregateId ActorId { get; }
}
