<script setup lang="ts">
import type { ButtonVariant } from "@/types/components";

defineProps<{
  disabled?: boolean;
  icon: string;
  loading?: boolean;
  text?: string;
  variant?: ButtonVariant;
}>();
</script>

<template>
  <icon-button :disabled="disabled" :icon="icon" :loading="loading" :text="text" type="submit" :variant="variant" />
</template>
