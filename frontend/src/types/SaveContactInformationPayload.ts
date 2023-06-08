import type { AddressInput } from "./AddressInput";
import type { EmailInput } from "./EmailInput";
import type { PhoneInput } from "./PhoneInput";

export type SaveContactInformationPayload = {
  address?: AddressInput;
  email: EmailInput;
  phone?: PhoneInput;
};
