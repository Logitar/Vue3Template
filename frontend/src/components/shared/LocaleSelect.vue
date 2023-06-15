<script setup lang="ts">
import { computed } from "vue";
import locales from "@/resources/locales.json";
import type { SelectOption } from "@/types/SelectOption";
import { orderBy } from "@/helpers/arrayUtils";

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
    label: "locale.label",
    placeholder: "locale.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() =>
  orderBy(
    locales.map(({ Name, NativeName }) => ({ value: Name, text: NativeName })),
    "text"
  )
);

defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
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
