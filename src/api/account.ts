import sleep from "./sleep";
import type { CurrentUser, SaveProfilePayload, SignInPayload, UserProfile } from "@/types/account";
import { useUserStore } from "@/stores/user";

export async function getProfile(): Promise<UserProfile> {
  await sleep(2500);
  const users = useUserStore();
  return users.getProfile();
}

export async function saveProfile(payload: SaveProfilePayload): Promise<UserProfile> {
  await sleep(2500);
  const users = useUserStore();
  return users.saveProfile(payload);
}

export async function signIn(payload: SignInPayload): Promise<CurrentUser> {
  await sleep(2500);
  const users = useUserStore();
  return users.signIn(payload);
}

export async function signOut(): Promise<void> {
  await sleep(2500);
  const users = useUserStore();
  users.signOut();
}
