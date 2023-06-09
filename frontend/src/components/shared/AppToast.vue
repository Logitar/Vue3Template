<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import type { ToastVariant } from "@/types/ToastVariant";

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
