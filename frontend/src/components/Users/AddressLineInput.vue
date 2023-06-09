<script setup lang="ts">
import { computed } from "vue";
import type { AddressLineType } from "@/types/AddressLineType";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
    type: AddressLineType;
    validate?: boolean;
  }>(),
  {
    disabled: false,
    required: false,
    validate: false,
  }
);

const inputId = computed<string>(() => props.id ?? `${props.type}Line`);
const inputLabel = computed<string>(() => props.label ?? `users.address.${props.type}.label`);
const inputPlaceholder = computed<string>(() => props.placeholder ?? `users.address.${props.type}.placeholder`);

defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="inputId"
    :label="inputLabel"
    :maxLength="validate ? 255 : undefined"
    :modelValue="modelValue"
    :placeholder="inputPlaceholder"
    :required="required"
    @update:modelValue="$emit('update:modelValue', $event)"
  />
</template>
