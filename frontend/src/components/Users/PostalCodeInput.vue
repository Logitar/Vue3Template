<script setup lang="ts">
import type { CountrySettings } from "@/types/CountrySettings";
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    country?: CountrySettings;
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    validate?: boolean;
  }>(),
  {
    disabled: false,
    id: "postalCode",
    label: "users.address.postalCode.label",
    placeholder: "users.address.postalCode.placeholder",
    required: false,
    validate: false,
  }
);

const rules = computed<any>(() => {
  const rules: any = {};
  const regex = props.country?.postalCode;
  if (regex) {
    rules.regex = regex;
  }
  return rules;
});

defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :maxLength="validate ? 255 : undefined"
    :modelValue="modelValue"
    :placeholder="placeholder"
    :required="required"
    :rules="rules"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
