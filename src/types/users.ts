import type { Actor, Aggregate } from "./aggregate";

export type Address = {
  street: string;
  locality: string;
  region?: string;
  postalCode?: string;
  country: string;
  formatted: string;
};

export type Contact = {
  isVerified: boolean;
  verifiedBy?: Actor;
  verifiedOn?: string;
};

export type Email = Contact & {
  address: string;
};

export type Phone = Contact & {
  countryCode?: string;
  number: string;
  extension?: string;
  e164Formatted: string;
};

export type User = Aggregate & {
  username: string;
  hasPassword: boolean;
  passwordChangedBy?: Actor;
  passwordChangedOn?: string;
  disabledBy?: Actor;
  disabledOn?: string;
  isDisabled: boolean;
  authenticatedOn?: string;
  address?: Address;
  email?: Email;
  phone?: Phone;
  isConfirmed: boolean;
  firstName?: string;
  middleName?: string;
  lastName?: string;
  fullName?: string;
  nickname?: string;
  birthdate?: string;
  gender?: string;
  locale?: string;
  timeZone?: string;
  picture?: string;
  profile?: string;
  website?: string;
};
