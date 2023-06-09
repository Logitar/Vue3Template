<script setup lang="ts">
import { computed } from "vue";
import timeZones from "@/resources/timeZones.json";
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
    id: "timeZone",
    label: "users.timeZone.label",
    placeholder: "users.timeZone.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() => {
  return orderBy(
    timeZones.map(({ id, displayName }) => ({ value: id, text: displayName })),
    "text"
  );
});

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
