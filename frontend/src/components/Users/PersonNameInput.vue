<script setup lang="ts">
import { computed } from "vue";
import type { PersonNameType } from "@/types/PersonNameType";

const props = withDefaults(
  defineProps<{
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    type: PersonNameType;
    validate?: boolean;
  }>(),
  {
    required: false,
    validate: false,
  }
);

const fieldId = computed<string>(() => props.id ?? `${props.type}Name`);
const fieldLabel = computed<string>(() => props.label ?? `users.name.${props.type}.label`);
const fieldPlaceholder = computed<string>(() => props.placeholder ?? `users.name.${props.type}.placeholder`);
</script>

<template>
  <form-input
    :id="fieldId"
    :label="fieldLabel"
    :maxLength="validate ? 255 : null"
    :modelValue="modelValue"
    :placeholder="fieldPlaceholder"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
