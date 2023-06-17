import type { ApiResult } from "@/types/ApiResult";
import type { CreateRealmPayload } from "@/types/CreateRealmPayload";
import type { Realm } from "@/types/Realm";
import type { SearchParameters } from "@/types/SearchParameters";
import type { SearchResults } from "@/types/SearchResults";
import type { UpdateRealmPayload } from "@/types/UpdateRealmPayload";
import { _delete, get, post, put } from ".";

export async function createRealm(payload: CreateRealmPayload): Promise<ApiResult<Realm>> {
  return await post("/realms", payload);
}

export async function deleteRealm(id: string): Promise<ApiResult<Realm>> {
  return await _delete(`/realms/${id}`);
}

export async function getRealm(id: string): Promise<ApiResult<Realm>> {
  return await get(`/realms/${id}`);
}

export async function searchRealms(params: SearchParameters): Promise<ApiResult<SearchResults<Realm>>> {
  return await post("/realms/search", params);
}

export async function updateRealm(id: string, payload: UpdateRealmPayload): Promise<ApiResult<Realm>> {
  return await put(`/realms/${id}`, payload);
}
