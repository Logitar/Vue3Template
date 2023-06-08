import type { ContactInput } from "./ContactInput";

export type PhoneInput = ContactInput & {
  countryCode?: string;
  number: string;
  extension?: string;
};
