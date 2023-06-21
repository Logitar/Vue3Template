import type { ApiError, ApiResult, GraphQLRequest, GraphQLResponse } from "@/types/api";
import { urlCombine } from "@/helpers/stringUtils";

const apiBaseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;
const contentType: string = "Content-Type";

async function execute<TData, TResult>(method: string, url: string, data?: TData): Promise<ApiResult<TResult>> {
  let body: string | undefined = undefined;
  const headers: HeadersInit = new Headers();
  if (data) {
    body = JSON.stringify(data);
    headers.set(contentType, "application/json; charset=UTF-8");
  }

  const input = urlCombine(apiBaseUrl, url);

  const response: Response = await fetch(input, { method, headers, body, credentials: "include" });

  let result: unknown = undefined;
  const resultType: string | null = response.headers.get(contentType);
  if (resultType) {
    if (resultType.includes("json")) {
      result = await response.json();
    } else if (resultType.includes("text")) {
      result = await response.text();
    } else {
      throw new Error(`The content type "${resultType}" is not supported.`);
    }
  }

  if (!response.ok) {
    const error: ApiError = { data: result, status: response.status };
    throw error;
  }

  return { data: result as TResult, status: response.status };
}

export async function _delete<TResult>(url: string): Promise<ApiResult<TResult>> {
  return await execute("DELETE", url);
}

export async function get<TResult>(url: string): Promise<ApiResult<TResult>> {
  return await execute("GET", url);
}

export async function patch<TData, TResult>(url: string, data?: TData): Promise<ApiResult<TResult>> {
  return await execute("PATCH", url, data);
}

export async function post<TData, TResult>(url: string, data?: TData): Promise<ApiResult<TResult>> {
  return await execute("POST", url, data);
}

export async function put<TData, TResult>(url: string, data?: TData): Promise<ApiResult<TResult>> {
  return await execute("PUT", url, data);
}

export async function graphQL<TVariables, TResult>(query: string, variables?: TVariables): Promise<ApiResult<TResult>> {
  const { data, status } = await post<GraphQLRequest<TVariables>, GraphQLResponse<TResult>>("/graphql", { query, variables });

  if (!data.data) {
    const error: ApiError = { data: data.errors, status };
    throw error;
  }

  const result: ApiResult<TResult> = { data: data.data, status };
  return result;
}
