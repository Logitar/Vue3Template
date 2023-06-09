using AutoMapper;
using Logitar.Demo.Core;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Infrastructure.Entities;
using System.Text.Json;

namespace Logitar.Demo.Infrastructure.Profiles;

internal class RealmProfile : Profile
{
  public RealmProfile()
  {
    CreateMap<RealmEntity, Realm>()
      .IncludeBase<AggregateEntity, Aggregate>()
      .ForMember(x => x.Id, x => x.MapFrom(MappingHelper.GetId))
      .ForMember(x => x.UsernameSettings, x => x.MapFrom(GetUsernameSettings))
      .ForMember(x => x.PasswordSettings, x => x.MapFrom(GetPasswordSettings))
      .ForMember(x => x.ClaimMappings, x => x.MapFrom(GetClaimMappings))
      .ForMember(x => x.CustomAttributes, x => x.MapFrom(MappingHelper.GetCustomAttributes));
    CreateMap<ReadOnlyUsernameSettings, UsernameSettings>();
    CreateMap<ReadOnlyPasswordSettings, PasswordSettings>();
  }

  private static IEnumerable<ClaimMapping> GetClaimMappings(RealmEntity realm, Realm _, IEnumerable<ClaimMapping> __, ResolutionContext context)
  {
    if (realm.ClaimMappings == null)
    {
      return Enumerable.Empty<ClaimMapping>();
    }

    Dictionary<string, ReadOnlyClaimMapping> claimMappings = JsonSerializer.Deserialize<Dictionary<string, ReadOnlyClaimMapping>>(realm.ClaimMappings)
      ?? throw new InvalidOperationException($"The claim mappings deserialization failed: '{realm.ClaimMappings}'.");

    return claimMappings.Select(pair => new ClaimMapping
    {
      Key = pair.Key,
      Type = pair.Value.Type,
      ValueType = pair.Value.ValueType
    });
  }

  private static PasswordSettings GetPasswordSettings(RealmEntity realm, Realm _, PasswordSettings __, ResolutionContext context)
  {
    ReadOnlyPasswordSettings passwordSettings = JsonSerializer.Deserialize<ReadOnlyPasswordSettings>(realm.PasswordSettings)
      ?? throw new InvalidOperationException($"The password settings deserialization failed: '{realm.PasswordSettings}'.");

    return context.Mapper.Map<PasswordSettings>(passwordSettings);
  }

  private static UsernameSettings GetUsernameSettings(RealmEntity realm, Realm _, UsernameSettings __, ResolutionContext context)
  {
    ReadOnlyUsernameSettings usernameSettings = JsonSerializer.Deserialize<ReadOnlyUsernameSettings>(realm.UsernameSettings)
      ?? throw new InvalidOperationException($"The username settings deserialization failed: '{realm.UsernameSettings}'.");

    return context.Mapper.Map<UsernameSettings>(usernameSettings);
  }
}
