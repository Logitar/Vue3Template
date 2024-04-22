<script setup lang="ts">
import AppInput from "@/components/shared/AppInput.vue";

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
    id="phone-extension"
    label="users.phone.extension"
    max="10"
    :model-value="modelValue"
    placeholder="users.phone.extension"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
