<script setup lang="ts">
import { ref } from "vue";
import { v4 as uuidv4 } from "uuid";
import type { ToastOptions } from "@/types/ToastOptions";
import AppToast from "./AppToast.vue";

type ToastWrapper = {
  id: string;
  options: ToastOptions;
};
const toasts = ref<ToastWrapper[]>([]);

function add(options: ToastOptions): void {
  const id = ["toast", uuidv4()].join("_");
  toasts.value.push({ id, options });
}

function onHidden(id: string): void {
  const index = toasts.value.findIndex((toast) => toast.id === id);
  if (index >= 0) {
    toasts.value.splice(index, 1);
  }
}

defineExpose({ add });
</script>

<template>
  <div class="toast-container position-fixed top-0 end-0 p-3">
    <AppToast
      v-for="toast in toasts"
      :id="toast.id"
      :key="toast.id"
      :message="toast.options.message"
      :solid="toast.options.solid"
      :title="toast.options.title"
      :variant="toast.options.variant"
      @hidden="onHidden(toast.id)"
    />
  </div>
</template>
