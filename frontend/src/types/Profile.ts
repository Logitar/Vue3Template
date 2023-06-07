import type { Email } from "./Email";
import type { Phone } from "./Phone";

export type Profile = {
  username: string;
  signedInOn?: Date;
  email: Email;
  phone?: Phone;
  firstName: string;
  middleName?: string;
  lastName: string;
  nickname?: string;
  fullName: string;
  locale: string;
  timeZone?: string;
  createdOn: Date;
  updatedOn: Date;
};
