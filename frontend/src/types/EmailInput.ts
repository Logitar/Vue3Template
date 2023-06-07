import type { ContactInput } from "./ContactInput";

export type EmailInput = ContactInput & {
  address: string;
};
