import type { Actor } from "@/types/aggregate";
import type { RecoverPasswordPayload, RegisterPayload, ResetPasswordPayload, SignInPayload } from "@/types/account";
import { useUserStore } from "@/stores/user";

function sleep(delay: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, delay));
}

export async function confirm(token: string): Promise<Actor | undefined> {
  await sleep(2500);
  const users = useUserStore();
  return users.verifyEmail(token);
}

export async function recoverPassword(payload: RecoverPasswordPayload): Promise<void> {
  await sleep(2500);
  console.log(payload);
}

export async function register(payload: RegisterPayload): Promise<void> {
  await sleep(2500);
  const users = useUserStore();
  users.create(payload);
}

export async function resetPassword(payload: ResetPasswordPayload): Promise<Actor | undefined> {
  await sleep(2500);
  const users = useUserStore();
  return users.resetPassword(payload);
}

export async function signIn(payload: SignInPayload): Promise<Actor> {
  await sleep(2500);
  const users = useUserStore();
  return users.signIn(payload);
}

export async function signOut(): Promise<void> {
  await sleep(2500);
}
