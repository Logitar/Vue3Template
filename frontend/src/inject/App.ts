import type { InjectionKey } from "vue";
import type { ToastOptions } from "@/types/ToastOptions";
import type { ToastUtils } from "@/types/ToastUtils";

export const handleErrorKey = Symbol() as InjectionKey<(e: any) => void>;
export const toastKey = Symbol() as InjectionKey<(toast: ToastOptions) => void>;
export const toastsKey = Symbol() as InjectionKey<ToastUtils>;
