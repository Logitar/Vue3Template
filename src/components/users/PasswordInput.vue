<script setup lang="ts">
import { computed, ref } from "vue";
import { useI18n } from "vue-i18n";

import AppInput from "@/components/shared/AppInput.vue";
import type { ConfirmedParams, ValidationRules } from "@/types/validation";
import type { PasswordSettings } from "@/types/settings";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    confirm?: ConfirmedParams<string>;
    id?: string;
    label?: string;
    modelValue?: string;
    required?: boolean | string;
    settings?: PasswordSettings;
  }>(),
  {
    id: "password",
    label: "users.password.label",
  },
);

const inputRef = ref<InstanceType<typeof AppInput> | null>();

const rules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.confirm) {
    rules.confirmed = [props.confirm.value, t(props.confirm.label).toLowerCase()];
  } else if (props.settings) {
    if (props.settings.minimumLength) {
      rules.min_length = props.settings.minimumLength;
    }
    if (props.settings.uniqueCharacters) {
      rules.unique_chars = props.settings.uniqueCharacters;
    }
    if (props.settings.requireNonAlphanumeric) {
      rules.require_non_alphanumeric = true;
    }
    if (props.settings.requireLowercase) {
      rules.require_lowercase = true;
    }
    if (props.settings.requireUppercase) {
      rules.require_uppercase = true;
    }
    if (props.settings.requireDigit) {
      rules.require_digit = true;
    }
  }
  return rules;
});

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();

function focus(): void {
  inputRef.value?.focus();
}
defineExpose({ focus });
</script>

<template>
  <AppInput
    floating
    :id="id"
    :label="label"
    :model-value="modelValue"
    :placeholder="label"
    ref="inputRef"
    :required="required"
    :rules="rules"
    type="password"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
