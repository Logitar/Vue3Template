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

const inputId = computed<string>(() => props.id ?? `${props.type}Name`);
const inputLabel = computed<string>(() => props.label ?? `users.name.${props.type}.label`);
const inputPlaceholder = computed<string>(() => props.placeholder ?? `users.name.${props.type}.placeholder`);

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-input
    :id="inputId"
    :label="inputLabel"
    :max-length="validate ? 255 : null"
    :model-value="modelValue"
    :placeholder="inputPlaceholder"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
