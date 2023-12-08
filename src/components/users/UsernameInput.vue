<script setup lang="ts">
import { computed } from "vue";
import type { UsernameSettings } from "@/types/realms";
import type { ValidationRules } from "@/types/validation";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    settings?: UsernameSettings;
    validate?: boolean;
  }>(),
  {
    disabled: false,
    id: "username",
    label: "users.name.user.label",
    placeholder: "users.name.user.placeholder",
    required: false,
    settings: () => ({ allowedCharacters: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" }),
    validate: false,
  }
);

const rules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.validate && props.settings?.allowedCharacters) {
    rules.username = props.settings.allowedCharacters;
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
    :max-length="validate ? 255 : null"
    :model-value="modelValue"
    :placeholder="placeholder"
    :required="required"
    :rules="rules"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
