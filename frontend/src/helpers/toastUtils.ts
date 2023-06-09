import { createApp, h } from "vue";
import i18n from "@/i18n";
import { Toast } from "bootstrap";
import AppToast from "@/components/shared/AppToast.vue";
import type { ToastOptions } from "@/types/ToastOptions";

import "@/assets/styles/toasts.css";

const containerId = "toast-container";
const stack: ToastOptions[] = [];

export function toast(options: ToastOptions): void {
  let container = document.getElementById(containerId);
  if (!container) {
    container = document.createElement("div");
    container.id = containerId;
    container.classList.add("toast-container");
    container.classList.add("position-fixed");
    container.classList.add("top-0");
    container.classList.add("end-0");
    container.classList.add("p-3");
    document.body.appendChild(container);
  }

  if (container) {
    const index = stack.push(options);
    const id = `bs-toast-${index}`;

    const component = createApp({
      setup() {
        return () =>
          h(AppToast, {
            id,
            message: options.message,
            solid: options.solid,
            title: options.title,
            variant: options.variant,
          });
      },
    });
    component.use(i18n);

    const wrapper = document.createElement("div");
    wrapper.classList.add("toast-wrapper");
    component.mount(wrapper);
    container.appendChild(wrapper);

    const element = document.getElementById(id);
    if (element) {
      const toast = Toast.getOrCreateInstance(element);
      toast.show();
    }
  }
}

export const toasts = {
  error(options?: ToastOptions): void {
    toast({
      ...{ message: "toasts.error.message", title: "toasts.error.title", variant: "danger" },
      ...options,
    });
  },
  success(message: string, options?: ToastOptions): void {
    toast({
      ...{ message, title: "toasts.success.title", variant: "success" },
      ...options,
    });
  },
};
