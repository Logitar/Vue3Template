import type { ApiResult } from "@/types/ApiResult";
import type { SearchParameters } from "@/types/SearchParameters";
import { post } from ".";

export async function searchRealms(params: SearchParameters): Promise<ApiResult> {
  return await post("/realms/search", params);
}
