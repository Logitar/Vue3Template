<script setup lang="ts">
withDefaults(
  defineProps<{
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
    id: "phoneExtension",
    label: "users.phone.extension.label",
    placeholder: "users.phone.extension.placeholder",
    required: false,
    validate: false,
  }
);

defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :maxLength="validate ? 16 : undefined"
    :modelValue="modelValue"
    :placeholder="placeholder"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
