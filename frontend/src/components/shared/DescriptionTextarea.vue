<script setup lang="ts">
withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    name?: string;
    placeholder?: string;
    rows?: number;
    required?: boolean;
  }>(),
  {
    disabled: false,
    id: "description",
    label: "description.label",
    placeholder: "description.placeholder",
    required: false,
    rows: 15,
    validate: false,
  }
);

defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
</script>

<template>
  <form-textarea
    :disabled="disabled"
    :id="id"
    :label="label"
    :modelValue="modelValue"
    :name="name"
    :placeholder="placeholder"
    :required="required"
    :rows="rows"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
