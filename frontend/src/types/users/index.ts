import type { Address, Email, Phone } from "./contact";

export type AuthenticatedUser = {
  displayName?: string;
  emailAddress?: string;
  picture?: string;
};

export type PersonNameType = "first" | "last" | "middle" | "nick";

export type ProfileUpdatedEvent = {
  toast?: boolean;
  user: UserProfile;
};

export type UserProfile = {
  username: string;
  passwordChangedOn?: string;
  signedInOn?: string;
  address?: Address;
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
