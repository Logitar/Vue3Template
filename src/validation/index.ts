import { configure, defineRule } from "vee-validate";
import { email, max, max_value, min, min_value, regex, required } from "@vee-validate/rules";
import { localize } from "@vee-validate/i18n";

import allowedCharacters from "./rules/allowedCharacters";
import confirmed from "./rules/confirmed";
import identifier from "./rules/identifier";
import requireDigit from "./rules/requireDigit";
import requireLowercase from "./rules/requireLowercase";
import requireNonAlphanumeric from "./rules/requireNonAlphanumeric";
import requireUppercase from "./rules/requireUppercase";
import slug from "./rules/slug";
import uniqueChars from "./rules/uniqueChars";
import url from "./rules/url";

defineRule("allowed_characters", allowedCharacters);
defineRule("confirmed", confirmed);
defineRule("email", email);
defineRule("identifier", identifier);
defineRule("max_length", max);
defineRule("max_value", max_value);
defineRule("min_length", min);
defineRule("min_value", min_value);
defineRule("regex", regex);
defineRule("require_digit", requireDigit);
defineRule("require_lowercase", requireLowercase);
defineRule("require_non_alphanumeric", requireNonAlphanumeric);
defineRule("require_uppercase", requireUppercase);
defineRule("required", required);
defineRule("slug", slug);
defineRule("unique_chars", uniqueChars);
defineRule("url", url);

import en from "./locales/errors.en.json";
import fr from "./locales/errors.fr.json";

configure({
  generateMessage: localize({
    en: { messages: en },
    fr: { messages: fr },
  }),
});
