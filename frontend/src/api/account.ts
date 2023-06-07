import type { ApiResult } from "@/types/ApiResult";
import type { ConfirmPayload } from "@/types/ConfirmPayload";
import type { RecoverPasswordPayload } from "@/types/RecoverPasswordPayload";
import type { RegisterPayload } from "@/types/RegisterPayload";
import type { ResetPasswordPayload } from "@/types/ResetPasswordPayload";
import type { SaveProfilePayload } from "./../types/SaveProfilePayload";
import type { SignInPayload } from "@/types/SignInPayload";
import { get, post, put } from ".";

export async function confirm(payload: ConfirmPayload): Promise<ApiResult> {
  return await post("/account/confirm", payload);
}

export async function getProfile(): Promise<ApiResult> {
  return await get("/account/profile");
}

export async function recoverPassword(payload: RecoverPasswordPayload): Promise<ApiResult> {
  return await post("/account/password/recover", payload);
}

export async function register(payload: RegisterPayload): Promise<ApiResult> {
  return await post("/account/register", payload);
}

export async function resetPassword(payload: ResetPasswordPayload): Promise<ApiResult> {
  return await post("/account/password/reset", payload);
}

export async function saveProfile(payload: SaveProfilePayload): Promise<ApiResult> {
  return await put("/account/profile", payload);
}

export async function signIn(payload: SignInPayload): Promise<ApiResult> {
  return await post("/account/sign/in", payload);
}

export async function signOut(): Promise<ApiResult> {
  return await post("/account/sign/out");
}

export async function validatePasswordReset(token: string): Promise<ApiResult> {
  return await get(`/account/password/reset?token=${token}`);
}
