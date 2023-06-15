import type { ToastOptions } from "./ToastOptions";

export type ToastUtils = {
  error: (options?: ToastOptions) => void;
  success: (message: string, options?: ToastOptions) => void;
  warning: (message: string, options?: ToastOptions) => void;
};
