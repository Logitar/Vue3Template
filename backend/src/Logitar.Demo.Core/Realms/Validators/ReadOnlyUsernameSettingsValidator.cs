using FluentValidation;

namespace Logitar.Demo.Core.Realms.Validators;

internal class ReadOnlyUsernameSettingsValidator : AbstractValidator<ReadOnlyUsernameSettings>
{
  public ReadOnlyUsernameSettingsValidator()
  {
    RuleFor(x => x.AllowedCharacters).NullOrNotEmpty();
  }
}
