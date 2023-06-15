<script setup lang="ts">
import type { SelectOption } from "@/types/SelectOption";

withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    isDescending?: boolean;
    label?: string;
    modelValue?: string;
    name?: string;
    options: SelectOption[];
    placeholder?: string;
  }>(),
  {
    disabled: false,
    isDescending: false,
    id: "sort",
    label: "sort.label",
    placeholder: "sort.placeholder",
  }
);

defineEmits<{
  (e: "isDescending", value: boolean): void;
  (e: "update:modelValue", value: string): void;
}>();
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :modelValue="modelValue"
    :name="name"
    noState
    :options="options"
    :placeholder="placeholder"
    @update:modelValue="$emit('update:modelValue', $event)"
  >
    <template #after>
      <form-checkbox id="isDescending" label="sort.isDescending" :modelValue="isDescending" @update:modelValue="$emit('isDescending', $event)" />
    </template>
  </form-select>
</template>
