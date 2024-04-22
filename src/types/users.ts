import type { Actor } from "./actor";

export type Address = Contact & {
  street: string;
  locality: string;
  postalCode?: string;
  region?: string;
  country: string;
  formatted: string;
};

export type AddressPayload = ContactPayload & {
  street: string;
  locality: string;
  postalCode?: string;
  region?: string;
  country: string;
};

export type Contact = {
  isVerified: boolean;
  verifiedBy?: Actor;
  verifiedOn?: string;
};

export type ContactPayload = {
  isVerified: boolean;
};

export type Email = Contact & {
  address: string;
};

export type EmailPayload = ContactPayload & {
  address: string;
};

export type Phone = Contact & {
  countryCode?: string;
  number: string;
  extension?: string;
  e164Formatted: string;
};

export type PhonePayload = ContactPayload & {
  countryCode?: string;
  number: string;
  extension?: string;
};
