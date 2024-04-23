<script setup lang="ts">
import { computed } from "vue";
import { parsingUtils } from "logitar-vue3-ui";

import AppInput from "@/components/shared/AppInput.vue";
import type { UsernameSettings } from "@/types/settings";
import type { ValidationRules } from "@/types/validation";

const { parseBoolean } = parsingUtils;

const props = defineProps<{
  disabled?: boolean | string;
  modelValue?: string;
  noStatus?: boolean | string;
  required?: boolean | string;
  settings?: UsernameSettings;
}>();

const rules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.settings?.allowedCharacters) {
    rules.allowed_characters = props.settings.allowedCharacters;
  }
  return rules;
});

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppInput
    :disabled="disabled"
    floating
    id="username"
    label="users.username"
    max="255"
    :model-value="modelValue"
    placeholder="users.username"
    :required="required"
    :rules="rules"
    :show-status="parseBoolean(noStatus) ? 'never' : undefined"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
