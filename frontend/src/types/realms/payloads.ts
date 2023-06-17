import type { ClaimMapping, PasswordSettings, UsernameSettings } from ".";
import type { CustomAttribute } from "@/types/_customAttribute";

export type CreateRealmPayload = {
  uniqueName: string;
  displayName?: string;
  description?: string;
  defaultLocale?: string;
  secret?: string;
  url?: string;
  requireConfirmedAccount: boolean;
  requireUniqueEmail: boolean;
  usernameSettings?: UsernameSettings;
  passwordSettings?: PasswordSettings;
  claimMappings?: ClaimMapping[];
  customAttributes?: CustomAttribute[];
};

export type UpdateRealmPayload = {
  displayName?: string;
  description?: string;
  defaultLocale?: string;
  secret?: string;
  url?: string;
  requireConfirmedAccount: boolean;
  requireUniqueEmail: boolean;
  usernameSettings?: UsernameSettings;
  passwordSettings?: PasswordSettings;
  claimMappings?: ClaimMapping[];
  customAttributes?: CustomAttribute[];
};
