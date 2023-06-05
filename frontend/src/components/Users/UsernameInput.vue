<script setup lang="ts">
import { computed } from "vue";
import type { UsernameSettings } from "@/types/UsernameSettings";

const props = withDefaults(
  defineProps<{
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    settings?: UsernameSettings;
    validate?: boolean;
  }>(),
  {
    id: "username",
    label: "users.name.user.label",
    placeholder: "users.name.user.placeholder",
    required: false,
    settings: () => ({ allowedCharacters: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" }),
    validate: false,
  }
);

const rules = computed<any>(() => {
  const rules: any = {};
  if (props.validate && props.settings?.allowedCharacters) {
    rules.username = props.settings.allowedCharacters;
  }
  return rules;
});
</script>

<template>
  <form-input
    :id="id"
    :label="label"
    :maxLength="validate ? 255 : null"
    :modelValue="modelValue"
    :placeholder="placeholder"
    :required="required"
    :rules="rules"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
