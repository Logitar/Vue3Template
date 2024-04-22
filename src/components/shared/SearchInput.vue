<script setup lang="ts">
import AppInput from "./AppInput.vue";

defineProps<{
  modelValue?: string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppInput
    floating
    id="search"
    label="search"
    :model-value="modelValue"
    placeholder="search"
    type="search"
    validation="server"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
