<script setup lang="ts">
import AppTextarea from "@/components/shared/AppTextarea.vue";

defineProps<{
  modelValue?: string;
  required?: boolean | string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppTextarea
    floating
    id="street-address"
    label="users.address.street"
    max="255"
    :model-value="modelValue"
    placeholder="users.address.street"
    :required="required"
    rows="3"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
