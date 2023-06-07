import type { EmailInput } from "./EmailInput";

export type SaveProfilePayload = {
  email: EmailInput;
  firstName: string;
  middleName?: string;
  lastName: string;
  nickname?: string;
  locale?: string;
};
