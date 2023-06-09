using FluentValidation;
using Logitar.Demo.Core.Realms.Events;

namespace Logitar.Demo.Core.Realms.Validators;

internal class RealmCreatedValidator : RealmSavedValidator<RealmCreated>
{
  public RealmCreatedValidator() : base()
  {
    RuleFor(x => x.UniqueName).NotEmpty()
      .MaximumLength(byte.MaxValue)
      .Slug();
  }
}
