<script setup lang="ts">
import { computed } from "vue";
import type { SelectOption } from "@/types/SelectOption";
import locales from "@/resources/locales.json";

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
    id: "locale",
    label: "users.locale.label",
    placeholder: "users.locale.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() => {
  return locales.map(({ Name, NativeName }) => ({ value: Name, text: NativeName })); // TODO(fpion): sort
});
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :modelValue="modelValue"
    :options="options"
    :placeholder="placeholder"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
