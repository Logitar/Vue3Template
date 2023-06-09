import i18n from "@/i18n";
import type { ToastOptions } from "@/types/ToastOptions";
import { Toast } from "bootstrap";

import "@/assets/styles/toasts.css";

const toasts: ToastOptions[] = [];
export function toast(options: ToastOptions): void {
  const container = document.getElementById("toast-container");
  if (container) {
    const index = toasts.push(options);

    const div = document.createElement("div");
    div.id = `bs-toast-${index}`;

    div.classList.add("toast");
    if (options.solid ?? true) {
      div.classList.add("toast-solid");
    }
    if (options.variant) {
      div.classList.add(`toast-${options.variant}`);
    }

    div.setAttribute("role", "alert");
    div.setAttribute("aria-live", "assertive");
    div.setAttribute("aria-atomic", "true");

    div.innerHTML = `
<div class="toast-header">
  <strong class="me-auto">${i18n.global.t(options.title)}</strong>
    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
</div>
<div class="toast-body">${i18n.global.t(options.message)}</div>
`;
    container.appendChild(div);

    const toast = Toast.getOrCreateInstance(div);
    toast.show();
  }
}
