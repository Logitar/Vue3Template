import type { ApiResult } from "@/types/ApiResult";
import type { SearchParameters } from "@/types/SearchParameters";
import { _delete, get, post, put } from ".";
import type { CreateRealmPayload } from "@/types/CreateRealmPayload";
import type { UpdateRealmPayload } from "@/types/UpdateRealmPayload";

export async function createRealm(payload: CreateRealmPayload): Promise<ApiResult> {
  return await post("/realms", payload);
}

export async function deleteRealm(id: string): Promise<ApiResult> {
  return await _delete(`/realms/${id}`);
}

export async function getRealm(id: string): Promise<ApiResult> {
  return await get(`/realms/${id}`);
}

export async function searchRealms(params: SearchParameters): Promise<ApiResult> {
  return await post("/realms/search", params);
}

export async function updateRealm(id: string, payload: UpdateRealmPayload): Promise<ApiResult> {
  return await put(`/realms/${id}`, payload);
}
