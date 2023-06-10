<script setup lang="ts">
import { provide, ref } from "vue";
import { RouterView } from "vue-router";
import AppFooter from "./components/Layout/AppFooter.vue";
import AppNavbar from "./components/Layout/AppNavbar.vue";
import ToastContainer from "./components/Layout/ToastContainer.vue";
import type { ToastOptions } from "./types/ToastOptions";
import type { ToastUtils } from "./types/ToastUtils";
import { handleErrorKey, toastKey, toastsKey } from "./inject/App";

const containerRef = ref<InstanceType<typeof ToastContainer> | null>(null);

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
};
provide(toastsKey, toasts);

function handleError(e: any): void {
  if (e) {
    console.error(e);
  }
  toasts.error();
}
provide(handleErrorKey, handleError);
</script>

<template>
  <AppNavbar environment="Development" />
  <RouterView />
  <AppFooter version="1.0.0" />
  <ToastContainer ref="containerRef" />
</template>
