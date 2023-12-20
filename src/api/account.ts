import type { Actor } from "@/types/aggregate";
import type { RegisterPayload } from "@/types/account";
import { useUserStore } from "@/stores/user";

export async function confirm(token: string): Promise<Actor | undefined> {
  const users = useUserStore();
  return users.verifyEmail(token);
}

export async function register(payload: RegisterPayload): Promise<void> {
  const users = useUserStore();
  users.create(payload);
}
