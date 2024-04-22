import sleep from "./sleep";
import type { CurrentUser, SignInPayload } from "@/types/account";
import { useUserStore } from "@/stores/user";

export async function signIn(payload: SignInPayload): Promise<CurrentUser> {
  await sleep(2500);
  const users = useUserStore();
  return users.signIn(payload);
}

export async function signOut(): Promise<void> {
  await sleep(2500);
}
