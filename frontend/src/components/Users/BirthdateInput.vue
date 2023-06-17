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
  (e: "update:model-value", value?: Date): void;
}>();
</script>

<template>
  <date-time-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :max="max"
    :min="min"
    :model-value="modelValue"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
