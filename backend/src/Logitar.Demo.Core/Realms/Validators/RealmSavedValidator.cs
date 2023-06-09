using FluentValidation;
using Logitar.Demo.Core.Realms.Events;

namespace Logitar.Demo.Core.Realms.Validators;

internal abstract class RealmSavedValidator<T> : AbstractValidator<T> where T : RealmSaved
{
  protected RealmSavedValidator()
  {
    RuleFor(x => x.DisplayName).NullOrNotEmpty()
      .MaximumLength(byte.MaxValue);

    RuleFor(x => x.Description).NullOrNotEmpty();

    RuleFor(x => x.DefaultLocale).Locale();

    RuleFor(x => x.Secret).NullOrNotEmpty()
      .MinimumLength(256 / 8)
      .MaximumLength(512 / 8);

    RuleFor(x => x.UsernameSettings).SetValidator(new ReadOnlyUsernameSettingsValidator());

    RuleFor(x => x.PasswordSettings).SetValidator(new ReadOnlyPasswordSettingsValidator());

    RuleForEach(x => x.ClaimMappings.Keys).NotEmpty()
      .MaximumLength(byte.MaxValue)
      .Identifier();

    RuleForEach(x => x.CustomAttributes.Keys).NotEmpty()
      .MaximumLength(byte.MaxValue)
      .Identifier();
    RuleForEach(x => x.CustomAttributes.Values).NotEmpty();
  }
}
