<script setup lang="ts">
withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    id: "profile",
    label: "users.profile.label",
    placeholder: "users.profile.placeholder",
    required: false,
  }
);
</script>

<template>
  <url-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :modelValue="modelValue"
    :placeholder="placeholder"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
