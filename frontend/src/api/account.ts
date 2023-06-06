import { post } from ".";
import type { ApiResult } from "@/types/ApiResult";
import type { ConfirmPayload } from "@/types/ConfirmPayload";
import type { RegisterPayload } from "@/types/RegisterPayload";

export async function confirm(payload: ConfirmPayload): Promise<ApiResult> {
  return await post("/account/confirm", payload);
}

export async function register(payload: RegisterPayload): Promise<ApiResult> {
  return await post("/account/register", payload);
}
