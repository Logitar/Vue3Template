import type { ToastOptions } from "logitar-vue3-ui";
import { defineStore } from "pinia";
import { nanoid } from "nanoid";
import { ref } from "vue";

import i18n from "@/i18n";

export const useToastStore = defineStore("toast", () => {
  const toasts = ref<ToastOptions[]>([]);

  function add(toast: ToastOptions): void {
    toasts.value.push(toast);
  }
  function remove(toast: ToastOptions): void {
    const index = toasts.value.findIndex(({ id }) => id === toast.id);
    if (index >= 0) {
      toasts.value.splice(index, 1);
    }
  }

  function toast(toast: ToastOptions): void {
    const { t } = i18n.global;
    toast = {
      close: toast.close ? t(toast.close) : t("actions.close"),
      duration: 15 * 1000,
      fade: true,
      id: `toast_${toast.id ?? nanoid()}`,
      solid: true,
      text: toast.text ? t(toast.text) : undefined,
      title: toast.title ? t(toast.title) : undefined,
      variant: toast.variant,
    };
    add(toast);
  }
  function error(text?: string): void {
    toast({
      text: text ?? "toasts.error.message",
      title: "toasts.error.title",
      variant: "danger",
    });
  }
  function success(text: string): void {
    toast({
      text,
      title: "toasts.success",
      variant: "success",
    });
  }
  function warning(text: string): void {
    toast({
      text,
      title: "toasts.warning.title",
      variant: "warning",
    });
  }

  return { toasts, add, remove, toast, error, success, warning };
});
