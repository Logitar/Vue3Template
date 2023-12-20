import type { RegisterPayload } from "@/types/account";
import { useUserStore } from "@/stores/user";

export async function register(payload: RegisterPayload): Promise<void> {
  const users = useUserStore();
  users.create(payload);
}
