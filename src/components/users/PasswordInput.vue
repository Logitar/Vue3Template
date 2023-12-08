<script setup lang="ts">
import { computed, ref } from "vue";
import { useI18n } from "vue-i18n";
import FormInput from "@/components/shared/FormInput.vue";
import type { ConfirmedParams } from "@/types/validation";
import type { PasswordSettings } from "@/types/realms";
import type { ValidationRules } from "@/types/validation";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    confirm?: ConfirmedParams<string>;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    settings?: PasswordSettings;
    validate?: boolean;
  }>(),
  {
    id: "password",
    label: "users.password.label",
    placeholder: "users.password.placeholder",
    required: false,
    settings: () => ({
      requiredLength: 6,
      requiredUniqueChars: 1,
      requireNonAlphanumeric: false,
      requireLowercase: true,
      requireUppercase: true,
      requireDigit: true,
    }),
    validate: false,
  }
);

const rules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.validate) {
    if (props.confirm) {
      rules.confirmed = [props.confirm.value, t(props.confirm.label).toLowerCase()];
    }
    if (props.settings) {
      const { requiredLength, requiredUniqueChars, requireNonAlphanumeric, requireLowercase, requireUppercase, requireDigit } = props.settings;
      if (requiredLength) {
        rules.min_length = requiredLength;
      }
      if (requiredUniqueChars) {
        rules.unique_chars = requiredUniqueChars;
      }
      if (requireNonAlphanumeric) {
        rules.require_non_alphanumeric = true;
      }
      if (requireLowercase) {
        rules.require_lowercase = true;
      }
      if (requireUppercase) {
        rules.require_uppercase = true;
      }
      if (requireDigit) {
        rules.require_digit = true;
      }
    }
  }
  return rules;
});

const inputRef = ref<InstanceType<typeof FormInput> | null>(null);
function focus(): void {
  inputRef.value?.focus();
}
defineExpose({ focus });

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <FormInput
    :id="id"
    :label="label"
    :model-value="modelValue"
    :placeholder="placeholder"
    ref="inputRef"
    :required="required"
    :rules="rules"
    type="password"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
