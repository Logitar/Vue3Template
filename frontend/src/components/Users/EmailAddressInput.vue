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
    id: "emailAddress",
    label: "users.email.address.label",
    placeholder: "users.email.address.placeholder",
    required: false,
    validate: false,
  }
);
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :maxLength="validate ? 255 : null"
    :modelValue="modelValue"
    :placeholder="placeholder"
    :required="required"
    type="email"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
