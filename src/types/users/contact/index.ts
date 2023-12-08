import type { Actor } from "@/types/actor";

export type AddressLineType = "street" | "additional";

export type Contact = {
  verifiedBy?: Actor;
  verifiedOn?: string;
  isVerified: boolean;
};

export type CountrySettings = {
  code: string;
  postalCode?: string;
  regions: string[];
};

export type Email = Contact & {
  address: string;
};

export type Address = Contact & {
  line1: string;
  line2?: string;
  locality: string;
  postalCode?: string;
  country: string;
  region?: string;
  formatted: string;
};

export type Phone = Contact & {
  countryCode?: string;
  number: string;
  extension?: string;
  e164Formatted: string;
};
