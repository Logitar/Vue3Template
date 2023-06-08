import type { ContactInput } from "./ContactInput";

export type AddressInput = ContactInput & {
  line1: string;
  line2?: string;
  locality: string;
  postalCode?: string;
  country: string;
  region?: string;
};
