import type { AddressInput } from "./AddressInput";
import type { EmailInput } from "./EmailInput";
import type { PhoneInput } from "./PhoneInput";

export type SaveProfilePayload = {
  address?: AddressInput;
  email: EmailInput;
  phone?: PhoneInput;
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
