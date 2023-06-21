import type { ApiResult, SearchParameters, SearchResults } from "@/types/api";
import type { CreateRealmPayload, UpdateRealmPayload } from "@/types/realms/payloads";
import type { Realm } from "@/types/realms";
import { _delete, get, graphQL, post, put } from ".";

export async function createRealm(payload: CreateRealmPayload): Promise<ApiResult<Realm>> {
  return await post("/realms", payload);
}

export async function deleteRealm(id: string): Promise<ApiResult<Realm>> {
  return await _delete(`/realms/${id}`);
}

export async function getRealm(id: string): Promise<ApiResult<Realm>> {
  return await get(`/realms/${id}`);
}

const searchRealmsQuery = `
query($parameters: RealmSearchParameters!) {
  realms(parameters: $parameters) {
    results {
      id
      uniqueName
      displayName
      updatedBy {
        id
        type
        isDeleted
        displayName
        emailAddress
        picture
      }
      updatedOn
    }
    total
  }
}
`;
type SearchRealmsRequest = {
  parameters: SearchParameters;
};
type SearchRealmsResponse = {
  realms: SearchResults<Realm>;
};
export async function searchRealms(parameters: SearchParameters): Promise<ApiResult<SearchResults<Realm>>> {
  const { data, status } = await graphQL<SearchRealmsRequest, SearchRealmsResponse>(searchRealmsQuery, { parameters });

  const result: ApiResult<SearchResults<Realm>> = { data: data.realms, status };
  return result;
}

export async function updateRealm(id: string, payload: UpdateRealmPayload): Promise<ApiResult<Realm>> {
  return await put(`/realms/${id}`, payload);
}
