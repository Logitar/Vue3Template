<script setup lang="ts">
import { computed, onMounted } from "vue";
import { useI18n } from "vue-i18n";
import { Toast } from "bootstrap";
import type { ToastVariant } from "@/types/components";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    id: string;
    message: string;
    solid?: boolean;
    title: string;
    variant?: ToastVariant;
  }>(),
  {
    solid: true,
  }
);

const classes = computed<string[]>(() => {
  const classes = ["toast"];
  if (props.solid) {
    classes.push("toast-solid");
  }
  if (props.variant) {
    classes.push(`toast-${props.variant}`);
  }
  return classes;
});

const emit = defineEmits<{
  (e: "hidden"): void;
}>();
onMounted(() => {
  const element = document.getElementById(props.id);
  if (element) {
    element.addEventListener("hidden.bs.toast", () => emit("hidden"));

    const toast = Toast.getOrCreateInstance(element);
    toast.show();
  }
});
</script>

<template>
  <div :class="classes" :id="id" role="alert" aria-live="assertive" aria-atomic="true">
    <div class="toast-header">
      <strong class="me-auto">{{ t(title) }}</strong>
      <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
    </div>
    <div class="toast-body">{{ t(message) }}</div>
  </div>
</template>
