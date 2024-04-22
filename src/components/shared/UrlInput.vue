<script setup lang="ts">
import type { InputSize } from "logitar-vue3-ui";

import type { ValidationRules } from "@/types/validation";
import AppInput from "./AppInput.vue";

defineProps<{
  describedBy?: string;
  disabled?: boolean | string;
  floating?: boolean | string;
  id?: string;
  label?: string;
  max?: number | string;
  min?: number | string;
  modelValue?: string;
  name?: string;
  pattern?: string;
  placeholder?: string;
  plaintext?: boolean | string;
  readonly?: boolean | string;
  required?: boolean | string;
  rules?: ValidationRules;
  size?: InputSize;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppInput
    :described-by="describedBy"
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label"
    :max="max"
    :min="min"
    :model-value="modelValue"
    :name="name"
    :pattern="pattern"
    :placeholder="placeholder"
    :plaintext="plaintext"
    :readonly="readonly"
    :required="required"
    :rules="rules"
    :size="size"
    type="url"
    @update:model-value="$emit('update:model-value', $event)"
  >
    <template #append>
      <a :class="{ 'btn btn-info text-white': true, disabled: !modelValue }" :href="modelValue" target="_blank">
        <font-awesome-icon icon="fas fa-arrow-up-right-from-square" />
      </a>
      <slot name="append"></slot>
    </template>
  </AppInput>
</template>
