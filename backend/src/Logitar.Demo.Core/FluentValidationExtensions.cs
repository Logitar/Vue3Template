using FluentValidation;
using System.Globalization;

namespace Logitar.Demo.Core;

internal static class FluentValidationExtensions
{
  public static IRuleBuilder<T, string?> Identifier<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder.Must(BeAValidIdentifier).WithErrorCode("IdentifierValidator")
      .WithMessage("'{PropertyName}' may only contain letters, digits and underscores, and may not start with a digit.");
  }
  private static bool BeAValidIdentifier(string? identifier) => identifier == null
    || (!string.IsNullOrEmpty(identifier) && !char.IsDigit(identifier.First()) && identifier.All(c => char.IsLetterOrDigit(c) || c == '_'));

  public static IRuleBuilder<T, CultureInfo?> Locale<T>(this IRuleBuilder<T, CultureInfo?> ruleBuilder)
  {
    return ruleBuilder.Must(BeAValidLocale).WithErrorCode("LocaleValidator")
      .WithMessage("'{PropertyName}' must be an instance of the CultureInfo class with a non-empty name and a LCID different from 4096.");
  }
  private static bool BeAValidLocale(CultureInfo? locale) => locale == null
    || (locale.LCID != 4096 && !string.IsNullOrWhiteSpace(locale.Name));

  public static IRuleBuilder<T, string?> NullOrNotEmpty<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder.Must(BeNullOrNotEmpty).WithErrorCode("NullOrNotEmptyValidator")
      .WithMessage("'{PropertyName}' must be null or a non-empty string.");
  }
  private static bool BeNullOrNotEmpty(string? s) => s == null || !string.IsNullOrWhiteSpace(s);

  public static IRuleBuilder<T, string?> Slug<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
    return ruleBuilder.Must(BeAValidSlug).WithErrorCode("SlugValidator")
      .WithMessage("'{PropertyName}' must be composed of non-empty alphanumeric words separated by hyphens.");
  }
  private static bool BeAValidSlug(string? slug) => slug == null
    || slug.Split('-').All(word => !string.IsNullOrEmpty(word) && word.All(char.IsLetterOrDigit));
}
