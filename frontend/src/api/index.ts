import type { ApiResult } from "@/types/ApiResult";
import { urlCombine } from "@/helpers/stringUtils";

const apiBaseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;
const contentType: string = "Content-Type";

async function execute(method: string, url: string, data?: any): Promise<ApiResult> {
  let body: string | undefined = undefined;
  const headers: HeadersInit = new Headers();
  if (data) {
    body = JSON.stringify(data);
    headers.set(contentType, "application/json; charset=UTF-8");
  }

  const input = urlCombine(apiBaseUrl, url);

  const response: Response = await fetch(input, { method, headers, body, credentials: "include" });

  const result: ApiResult = { status: response.status };

  const dataType: string | null = response.headers.get(contentType);
  if (dataType) {
    if (dataType.includes("json")) {
      result.data = await response.json();
    } else if (dataType.includes("text")) {
      result.data = await response.text();
    } else {
      throw new Error(`The content type "${dataType}" is not supported.`);
    }
  }

  if (!response.ok) {
    throw result;
  }

  return result;
}

export async function _delete(url: string): Promise<ApiResult> {
  return await execute("DELETE", url);
}

export async function get(url: string): Promise<ApiResult> {
  return await execute("GET", url);
}

export async function patch(url: string, data?: any): Promise<ApiResult> {
  return await execute("PATCH", url, data);
}

export async function post(url: string, data?: any): Promise<ApiResult> {
  return await execute("POST", url, data);
}

export async function put(url: string, data?: any): Promise<ApiResult> {
  return await execute("PUT", url, data);
}
