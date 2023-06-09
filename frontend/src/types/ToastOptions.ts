import type { ToastVariant } from "./ToastVariant";

export type ToastOptions = {
  message: string;
  solid?: boolean;
  title: string;
  variant?: ToastVariant;
};
