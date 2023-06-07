import type { EmailInput } from "./EmailInput";

export type SaveProfilePayload = {
  email: EmailInput;
  firstName: string;
  middleName?: string;
  lastName: string;
  nickname?: string;
  birthdate?: Date;
  gender?: string;
  locale: string;
  timeZone?: string;
  picture?: string;
  profile?: string;
  website?: string;
};
