import type { Contact } from "./Contact";

export type Address = Contact & {
  line1: string;
  line2?: string;
  locality: string;
  postalCode?: string;
  country: string;
  region?: string;
  formatted: string;
};
