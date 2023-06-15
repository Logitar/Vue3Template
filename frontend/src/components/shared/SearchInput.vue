<script setup lang="ts">
withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    name?: string;
    placeholder?: string;
  }>(),
  {
    disabled: false,
    id: "search",
    label: "search.label",
    placeholder: "search.placeholder",
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
    :modelValue="modelValue"
    :name="name"
    noState
    :placeholder="placeholder"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
