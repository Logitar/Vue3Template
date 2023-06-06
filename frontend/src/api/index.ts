import type { ApiResult } from "@/types/ApiResult";

const baseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;
const contentType: string = "Content-Type";

async function execute(method: string, url: string, data?: any): Promise<ApiResult> {
  const request: RequestInit = { method };
  if (data) {
    request.body = JSON.stringify(data);
    request.headers = new Headers();
    request.headers.set(contentType, "application/json; charset=UTF-8");
  }
  const response: Response = await fetch([baseUrl.replace(/\/+$/g, ""), url.replace(/^\/+/g, "")].join("/"), request);
  const result: ApiResult = { status: response.status };
  const dataType: string | null = response.headers.get(contentType);
  if (dataType) {
    if (dataType.includes("json")) {
      result.data = await response.json();
    } else {
      throw new Error(`The content type "${dataType}" is not supported.`);
    }
  }
  if (!response.ok) {
    throw result;
  }
  return result;
}

export async function post(url: string, data?: any): Promise<ApiResult> {
  return await execute("POST", url, data);
}
