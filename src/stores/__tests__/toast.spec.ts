import type { ToastOptions } from "logitar-vue3-ui";
import { beforeEach, describe, expect, it } from "vitest";
import { createPinia, setActivePinia } from "pinia";

import { useToastStore } from "../toast";

const toast: ToastOptions = {
  duration: 10 * 1000,
  fade: true,
  text: "Hello World!",
  title: "Message",
  variant: "info",
};

describe("toastStore", () => {
  beforeEach(() => {
    setActivePinia(createPinia());
  });

  it("should add a toast to the list", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.add(toast);
    expect(toasts.toasts.length).toBe(1);
    expect(toasts.toasts.at(0)?.id).toBe(toast.id);
  });

  it("should create a toast from the specified options", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.toast({
      text: "notFound.help",
      title: "toasts.success",
      variant: "info",
    });
    const toast: ToastOptions | undefined = toasts.toasts.at(0);
    if (toast) {
      expect(toast.close).toBe("Close");
      expect(toast.duration).toBe(15000);
      expect(toast.fade).toBe(true);
      expect(toast.id).toMatch(/^toast_/);
      expect(toast.solid).toBe(true);
      expect(toast.text).toBe("The requested page could not be found.");
      expect(toast.title).toBe("Success!");
      expect(toast.variant).toBe("info");
    } else {
      expect(toast).toBeDefined();
    }
  });

  it("should create a success toast", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.success("notFound.lead");
    const toast: ToastOptions | undefined = toasts.toasts.at(0);
    if (toast) {
      expect(toast.text).toBe("Page Not Found");
      expect(toast.title).toBe("Success!");
      expect(toast.variant).toBe("success");
    } else {
      expect(toast).toBeDefined();
    }
  });

  it("should create a warning toast", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.warning("toasts.warning.signedOut");
    const toast: ToastOptions | undefined = toasts.toasts.at(0);
    if (toast) {
      expect(toast.text).toBe("You have been signed-out, please try sign-in to your account again.");
      expect(toast.title).toBe("Warning!");
      expect(toast.variant).toBe("warning");
    } else {
      expect(toast).toBeDefined();
    }
  });

  it("should create an error toast", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.error("notFound.link");
    const toast: ToastOptions | undefined = toasts.toasts.at(0);
    if (toast) {
      expect(toast.text).toBe("Go to Home");
      expect(toast.title).toBe("Error!");
      expect(toast.variant).toBe("danger");
    } else {
      expect(toast).toBeDefined();
    }
  });

  it("should create the default error toast", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.error();
    const toast: ToastOptions | undefined = toasts.toasts.at(0);
    if (toast) {
      expect(toast.text).toBe("An unexpected error occurred. Please try again later or contact our support team.");
      expect(toast.title).toBe("Error!");
      expect(toast.variant).toBe("danger");
    } else {
      expect(toast).toBeDefined();
    }
  });

  it("should remove a toast from the list", () => {
    const toasts = useToastStore();
    expect(toasts.toasts.length).toBe(0);

    toasts.add(toast);
    expect(toasts.toasts.at(0)?.id).toBe(toast.id);

    toasts.remove(toast);
    expect(toasts.toasts.length).toBe(0);
  });
});
