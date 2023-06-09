using AutoMapper;
using Logitar.Demo.Core.Actors;
using Logitar.Demo.Web.Models.Account;

using PortalActors = Logitar.Portal.Contracts.Actors;
using PortalContact = Logitar.Portal.Contracts.Users.Contact;
using PortalUsers = Logitar.Portal.Contracts.Users;

namespace Logitar.Demo.Web.Profiles;

internal class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<PortalUsers.User, UserProfile>()
      .ForMember(x => x.PasswordChangedOn, x => x.MapFrom(GetPasswordChangedOn))
      .ForMember(x => x.Email, x => x.MapFrom(GetEmail))
      .ForMember(x => x.FirstName, x => x.MapFrom(GetFirstName))
      .ForMember(x => x.LastName, x => x.MapFrom(GetLastName))
      .ForMember(x => x.FullName, x => x.MapFrom(GetFullName))
      .ForMember(x => x.Locale, x => x.MapFrom(GetLocale));

    CreateMap<PortalContact.Address, Address>();
    CreateMap<PortalContact.Contact, Contact>();
    CreateMap<PortalContact.Email, Email>();
    CreateMap<PortalContact.Phone, Phone>();

    CreateMap<PortalActors.Actor, Actor>()
      .ForMember(x => x.Type, x => x.MapFrom(GetActorType));
  }

  private static DateTime GetPasswordChangedOn(PortalUsers.User user, UserProfile _)
  {
    return user.PasswordChangedOn ?? throw new InvalidOperationException($"The {nameof(user.PasswordChangedOn)} is required.");
  }

  private static Email GetEmail(PortalUsers.User user, UserProfile _, Email __, ResolutionContext context)
  {
    if (user.Email == null)
    {
      throw new InvalidOperationException($"The {nameof(user.Email)} is required.");
    }

    return context.Mapper.Map<Email>(user.Email);
  }

  private static string GetFirstName(PortalUsers.User user, UserProfile _)
  {
    return user.FirstName ?? throw new InvalidOperationException($"The {nameof(user.FirstName)} is required.");
  }

  private static string GetLastName(PortalUsers.User user, UserProfile _)
  {
    return user.LastName ?? throw new InvalidOperationException($"The {nameof(user.LastName)} is required.");
  }

  private static string GetFullName(PortalUsers.User user, UserProfile _)
  {
    return user.FullName ?? throw new InvalidOperationException($"The {nameof(user.FullName)} is required.");
  }

  private static string GetLocale(PortalUsers.User user, UserProfile _)
  {
    return user.Locale ?? throw new InvalidOperationException($"The {nameof(user.Locale)} is required.");
  }

  private static ActorType GetActorType(PortalActors.Actor actor, Actor _)
  {
    return actor.Type == PortalActors.ActorType.User ? ActorType.User : ActorType.System;
  }
}
