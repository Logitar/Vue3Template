<script setup lang="ts">
import AppTextarea from "./AppTextarea.vue";

defineProps<{
  modelValue?: string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppTextarea
    floating
    id="description"
    label="description"
    :model-value="modelValue"
    placeholder="description"
    rows="15"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
