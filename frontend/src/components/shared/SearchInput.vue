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
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :model-value="modelValue"
    :name="name"
    no-state
    :placeholder="placeholder"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
