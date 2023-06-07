import type { Email } from "./Email";
import type { Phone } from "./Phone";

export type UserProfile = {
  username: string;
  signedInOn?: string;
  email: Email;
  phone?: Phone;
  firstName: string;
  middleName?: string;
  lastName: string;
  nickname?: string;
  fullName: string;
  birthdate?: string;
  gender?: string;
  locale: string;
  timeZone?: string;
  picture?: string;
  profile?: string;
  website?: string;
  createdOn: string;
  updatedOn: string;
};
