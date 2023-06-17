export type AddressPayload = ContactPayload & {
  line1: string;
  line2?: string;
  locality: string;
  postalCode?: string;
  country: string;
  region?: string;
};

export type ContactPayload = {
  verify: boolean;
};

export type EmailPayload = ContactPayload & {
  address: string;
};

export type PhonePayload = ContactPayload & {
  countryCode?: string;
  number: string;
  extension?: string;
};
