import { post } from ".";
import type { ApiResult } from "@/types/ApiResult";
import type { ConfirmPayload } from "@/types/ConfirmPayload";
import type { RegisterPayload } from "@/types/RegisterPayload";
import type { SignInPayload } from "@/types/SignInPayload";

export async function confirm(payload: ConfirmPayload): Promise<ApiResult> {
  return await post("/account/confirm", payload);
}

export async function register(payload: RegisterPayload): Promise<ApiResult> {
  return await post("/account/register", payload);
}

export async function signIn(payload: SignInPayload): Promise<ApiResult> {
  return await post("/account/sign/in", payload);
}

export async function signOut(): Promise<ApiResult> {
  return await post("/account/sign/out");
}
