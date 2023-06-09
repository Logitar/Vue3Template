using Logitar.Demo.Core;
using Logitar.Demo.Core.Actors;
using Logitar.Demo.Infrastructure.Actors;
using Logitar.Demo.Infrastructure.Entities;
using Logitar.EventSourcing;
using System.Text.Json;

namespace Logitar.Demo.Infrastructure.Profiles;

internal static class MappingHelper
{
  public static Actor? GetActor(Guid? id, string? json)
  {
    if (!id.HasValue || json == null)
    {
      return null;
    }

    ActorInfo entity = ActorInfo.Deserialize(json);

    return entity.ToActor(id.Value);
  }

  public static IEnumerable<CustomAttribute> GetCustomAttributes(ICustomAttributes entity, object? _)
  {
    if (entity.CustomAttributes == null)
    {
      return Enumerable.Empty<CustomAttribute>();
    }

    Dictionary<string, string> customAttributes = JsonSerializer.Deserialize<Dictionary<string, string>>(entity.CustomAttributes)
      ?? throw new InvalidOperationException($"The custom attributes deserialization failed: '{entity.CustomAttributes}'.");

    return customAttributes.Select(pair => new CustomAttribute
    {
      Key = pair.Key,
      Value = pair.Value
    });
  }

  public static Guid GetId(AggregateEntity entity, object? _)
  {
    return new AggregateId(entity.AggregateId).ToGuid();
  }
}
