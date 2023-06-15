import type { ClaimMapping } from "./ClaimMapping";
import type { CustomAttribute } from "./CustomAttribute";
import type { PasswordSettings } from "./PasswordSettings";
import type { UsernameSettings } from "./UsernameSettings";

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
