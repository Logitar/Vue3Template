<script setup lang="ts">
import { RouterView, useRoute, useRouter } from "vue-router";
import { Tooltip } from "bootstrap";
import { provide, ref } from "vue";

import AppFooter from "./components/layout/AppFooter.vue";
import AppNavbar from "./components/layout/AppNavbar.vue";
import ToastContainer from "./components/layout/ToastContainer.vue";
import type { ApiError, GraphQLError } from "./types/api";
import type { ToastOptions, ToastUtils } from "./types/components";
import { handleErrorKey, registerTooltipsKey, toastKey, toastsKey } from "./inject/App";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const route = useRoute();
const router = useRouter();

const containerRef = ref<InstanceType<typeof ToastContainer> | null>(null);

function handleError(e: unknown): void {
  if (e) {
    const { data, status } = e as ApiError;
    if (status === 401 || (data as GraphQLError[])?.some((error) => error.extensions?.code === "ACCESS_DENIED") === true) {
      account.signOut();
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
  <AppNavbar />
  <RouterView />
  <AppFooter />
  <ToastContainer ref="containerRef" />
</template>
