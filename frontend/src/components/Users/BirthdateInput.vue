<script setup lang="ts">
withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: Date;
    required?: boolean;
  }>(),
  {
    disabled: false,
    id: "birthdate",
    label: "users.birthdate",
    required: false,
  }
);

const max = new Date();
const min = new Date(max);
min.setFullYear(min.getFullYear() - 100);

defineEmits<{
  (e: "update:modelValue", value?: Date): void;
}>();
</script>

<template>
  <date-time-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :max="max"
    :min="min"
    :modelValue="modelValue"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
