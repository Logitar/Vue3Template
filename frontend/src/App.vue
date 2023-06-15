<script setup lang="ts">
import { provide, ref } from "vue";
import { RouterView, useRoute, useRouter } from "vue-router";
import { Tooltip } from "bootstrap";
import AppFooter from "./components/Layout/AppFooter.vue";
import AppNavbar from "./components/Layout/AppNavbar.vue";
import ToastContainer from "./components/Layout/ToastContainer.vue";
import type { ApiResult } from "./types/ApiResult";
import type { ToastOptions } from "./types/ToastOptions";
import type { ToastUtils } from "./types/ToastUtils";
import { handleErrorKey, registerTooltipsKey, toastKey, toastsKey } from "./inject/App";

const route = useRoute();
const router = useRouter();

const containerRef = ref<InstanceType<typeof ToastContainer> | null>(null);

function handleError(e: any): void {
  if (e) {
    const { status } = e as ApiResult;
    if (status === 401) {
      toasts.warning("toasts.warning.signedOut");
      router.push({ name: "SignIn", query: { redirect: route.fullPath } });
    } else {
      console.error(e);
      toasts.error();
    }
  } else {
    toasts.error();
  }
}
provide(handleErrorKey, handleError);

function registerTooltips(): void {
  const tooltipTriggers = document.querySelectorAll('[data-bs-toggle="tooltip"]');
  tooltipTriggers.forEach((element) => new Tooltip(element));
}
provide(registerTooltipsKey, registerTooltips);

function toast(toast: ToastOptions): void {
  if (containerRef.value) {
    containerRef.value.add(toast);
  }
}
provide(toastKey, toast);

const toasts: ToastUtils = {
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
  warning(message: string, options?: ToastOptions): void {
    toast({
      ...{ message, title: "toasts.warning.title", variant: "warning" },
      ...options,
    });
  },
};
provide(toastsKey, toasts);
</script>

<template>
  <AppNavbar environment="Development" />
  <RouterView />
  <AppFooter version="1.0.0" />
  <ToastContainer ref="containerRef" />
</template>
