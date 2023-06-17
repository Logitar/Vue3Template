import type { ApiResult } from "@/types/api";
import type {
  ChangePasswordPayload,
  ConfirmPayload,
  RecoverPasswordPayload,
  RegisterPayload,
  ResetPasswordPayload,
  SaveContactInformationPayload,
  SavePersonalInformationPayload,
  SignInPayload,
} from "@/types/users/payloads";
import type { UserProfile } from "@/types/users";
import { get, post, put } from ".";

export async function changePassword(payload: ChangePasswordPayload): Promise<ApiResult<UserProfile>> {
  return await post("/account/password/change", payload);
}

export async function confirm(payload: ConfirmPayload): Promise<ApiResult<void>> {
  return await post("/account/confirm", payload);
}

export async function getProfile(): Promise<ApiResult<UserProfile>> {
  return await get("/account/profile");
}

export async function recoverPassword(payload: RecoverPasswordPayload): Promise<ApiResult<void>> {
  return await post("/account/password/recover", payload);
}

export async function register(payload: RegisterPayload): Promise<ApiResult<void>> {
  return await post("/account/register", payload);
}

export async function resetPassword(payload: ResetPasswordPayload): Promise<ApiResult<void>> {
  return await post("/account/password/reset", payload);
}

export async function saveContactInformation(payload: SaveContactInformationPayload): Promise<ApiResult<UserProfile>> {
  return await put("/account/profile/contact", payload);
}

export async function savePersonalInformation(payload: SavePersonalInformationPayload): Promise<ApiResult<UserProfile>> {
  return await put("/account/profile/personal", payload);
}

export async function signIn(payload: SignInPayload): Promise<ApiResult<UserProfile>> {
  return await post("/account/sign/in", payload);
}

export async function signOut(): Promise<ApiResult<void>> {
  return await post("/account/sign/out");
}

export async function validatePasswordReset(token: string): Promise<ApiResult<void>> {
  return await get(`/account/password/reset?token=${token}`);
}
