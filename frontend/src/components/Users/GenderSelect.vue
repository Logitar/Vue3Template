<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import type { SelectOption } from "@/types/SelectOption";

const { tm } = useI18n();

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
    id: "gender",
    label: "users.gender.label",
    placeholder: "users.gender.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() => {
  return Object.entries(tm("users.gender.options")).map(([value, text]) => ({ text, value } as SelectOption)); // TODO(fpion): sort
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
