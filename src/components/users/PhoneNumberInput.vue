<script setup lang="ts">
import AppInput from "@/components/shared/AppInput.vue";

defineProps<{
  modelValue?: string;
  required?: boolean | string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppInput
    floating
    id="phone-number"
    label="users.phone.number"
    max="20"
    :model-value="modelValue"
    placeholder="users.phone.number"
    :required="required"
    type="tel"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
