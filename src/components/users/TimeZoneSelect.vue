<script setup lang="ts">
import { computed } from "vue";
import timeZones from "@/resources/timeZones.json";
import type { SelectOption } from "@/types/components";
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

const options = computed<SelectOption[]>(() =>
  orderBy(
    timeZones.map(({ id, displayName }) => ({ value: id, text: displayName })),
    "text"
  )
);

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :model-value="modelValue"
    :options="options"
    :placeholder="placeholder"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
