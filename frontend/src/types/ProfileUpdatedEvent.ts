import type { UserProfile } from "./UserProfile";

export type ProfileUpdatedEvent = {
  toast?: boolean;
  user: UserProfile;
};
