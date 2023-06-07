import type { Contact } from "./Contact";

export type Email = Contact & {
  address: string;
};
