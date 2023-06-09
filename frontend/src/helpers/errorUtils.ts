import { toasts } from "./toastUtils";

export function handleError(e: any): void {
  if (e) {
    console.error(e);
  }
  toasts.error();
}
