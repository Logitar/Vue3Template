<script setup lang="ts">
import type { InputSize } from "logitar-vue3-ui";

import AppInput from "./AppInput.vue";
import type { ValidationRules } from "@/types/validation";
import { toDateTimeLocal } from "@/helpers/dateUtils";

defineProps<{
  describedBy?: string;
  disabled?: boolean | string;
  floating?: boolean | string;
  id?: string;
  label?: string;
  max?: Date;
  min?: Date;
  modelValue?: Date;
  name?: string;
  plaintext?: boolean | string;
  readonly?: boolean | string;
  required?: boolean | string;
  rules?: ValidationRules;
  size?: InputSize;
  step?: number | string;
}>();

const emit = defineEmits<{
  (e: "update:model-value", value?: Date): void;
}>();
function onModelValueUpdate(value: string): void {
  try {
    const date = new Date(value);
    emit("update:model-value", isNaN(date.getTime()) ? undefined : date);
  } catch (e: unknown) {
    emit("update:model-value", undefined);
  }
}
</script>

<template>
  <AppInput
    :described-by="describedBy"
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label"
    :max="max ? toDateTimeLocal(max) : undefined"
    :min="min ? toDateTimeLocal(min) : undefined"
    :model-value="modelValue ? toDateTimeLocal(modelValue) : undefined"
    :name="name"
    :plaintext="plaintext"
    :readonly="readonly"
    :required="required"
    :rules="rules"
    :size="size"
    :step="step"
    type="datetime-local"
    @update:model-value="onModelValueUpdate"
  />
</template>
