import type { Aggregate } from "@/types/aggregate";
import type { CustomAttribute } from "@/types/customAttribute";

export type ClaimMapping = {
  key: string;
  type: string;
  valueType?: string;
};

export type PasswordSettings = {
  requiredLength: number;
  requiredUniqueChars: number;
  requireNonAlphanumeric: boolean;
  requireLowercase: boolean;
  requireUppercase: boolean;
  requireDigit: boolean;
};

export type Realm = Aggregate & {
  id: string;
  uniqueName: string;
  displayName?: string;
  description?: string;
  defaultLocale?: string;
  secret: string;
  url?: string;
  requireConfirmedAccount: boolean;
  requireUniqueEmail: boolean;
  usernameSettings: UsernameSettings;
  passwordSettings: PasswordSettings;
  claimMappings: ClaimMapping[];
  customAttributes: CustomAttribute[];
};

export type UsernameSettings = {
  allowedCharacters?: string;
};
