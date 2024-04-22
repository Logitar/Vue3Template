<script setup lang="ts">
import AppInput from "@/components/shared/AppInput.vue";
import type { CountrySettings } from "@/types/settings";

defineProps<{
  country?: CountrySettings;
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
    id="postal-code"
    label="users.address.postalCode"
    max="255"
    :model-value="modelValue"
    :pattern="country?.postalCode"
    placeholder="users.address.postalCode"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
