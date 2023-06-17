<script setup lang="ts">
import type { CountrySettings } from "@/types/CountrySettings";
import type { ValidationRules } from "@/types/ValidationRules";
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

const rules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  const regex = props.country?.postalCode;
  if (regex) {
    rules.regex = regex;
  }
  return rules;
});

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :max-length="validate ? 255 : undefined"
    :model-value="modelValue"
    :placeholder="placeholder"
    :required="required"
    :rules="rules"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
