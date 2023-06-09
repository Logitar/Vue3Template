import { toast } from "./toastUtils";

export function handleError(e: any): void {
  if (e) {
    console.error(e);
  }
  toast({ message: "toasts.error.message", title: "toast.error.title", variant: "danger" });
}
