import type { ApiResult } from "@/types/ApiResult";
import type { ChangePasswordPayload } from "@/types/ChangePasswordPayload";
import type { ConfirmPayload } from "@/types/ConfirmPayload";
import type { RecoverPasswordPayload } from "@/types/RecoverPasswordPayload";
import type { RegisterPayload } from "@/types/RegisterPayload";
import type { ResetPasswordPayload } from "@/types/ResetPasswordPayload";
import type { SaveContactInformationPayload } from "@/types/SaveContactInformationPayload";
import type { SavePersonalInformationPayload } from "@/types/SavePersonalInformationPayload";
import type { SignInPayload } from "@/types/SignInPayload";
import { get, post, put } from ".";

export async function changePassword(payload: ChangePasswordPayload): Promise<ApiResult> {
  return await post("/account/password/change", payload);
}

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

export async function saveContactInformation(payload: SaveContactInformationPayload): Promise<ApiResult> {
  return await put("/account/profile/contact", payload);
}

export async function savePersonalInformation(payload: SavePersonalInformationPayload): Promise<ApiResult> {
  return await put("/account/profile/personal", payload);
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
