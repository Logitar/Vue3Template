<script setup lang="ts">
import type { SelectOption } from "@/types/components";

withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    descending?: boolean;
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
  (e: "descending", value: boolean): void;
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :model-value="modelValue"
    :name="name"
    no-state
    :options="options"
    :placeholder="placeholder"
    @update:model-value="$emit('update:model-value', $event)"
  >
    <template #after>
      <form-checkbox id="isDescending" label="sort.isDescending" :model-value="descending" @update:model-value="$emit('descending', $event)" />
    </template>
  </form-select>
</template>
