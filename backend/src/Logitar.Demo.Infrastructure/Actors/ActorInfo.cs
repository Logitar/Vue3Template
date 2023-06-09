using Logitar.Demo.Core.Actors;
using Logitar.Demo.Infrastructure.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logitar.Demo.Infrastructure.Actors;

internal record ActorInfo
{
  private static readonly JsonSerializerOptions _options = new();
  static ActorInfo() => _options.Converters.Add(new JsonStringEnumConverter());

  public static ActorInfo System { get; } = new();

  public ActorType Type { get; init; }
  public bool IsDeleted { get; init; }

  public string DisplayName { get; init; } = ActorType.System.ToString();
  public string? EmailAddress { get; init; }
  public string? Picture { get; init; }

  public static ActorInfo From(Actor actor) => new()
  {
    Type = actor.Type,
    IsDeleted = actor.IsDeleted,
    DisplayName = actor.DisplayName,
    EmailAddress = actor.EmailAddress,
    Picture = actor.Picture
  };
  public static ActorInfo From(ActorEntity actor) => new()
  {
    Type = actor.Type,
    IsDeleted = actor.IsDeleted,
    DisplayName = actor.DisplayName,
    EmailAddress = actor.EmailAddress,
    Picture = actor.Picture
  };

  public static ActorInfo Deserialize(string json) => JsonSerializer.Deserialize<ActorInfo>(json, _options)
    ?? throw new InvalidOperationException($"The actor deserialization failed: '{json}'.");
  public string Serialize() => JsonSerializer.Serialize(this, _options);

  public Actor ToActor(Guid id) => new()
  {
    Id = id,
    Type = Type,
    IsDeleted = IsDeleted,
    DisplayName = DisplayName,
    EmailAddress = EmailAddress,
    Picture = Picture
  };
}
