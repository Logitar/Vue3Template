<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import type { SelectOption } from "@/types/SelectOption";
import { orderBy } from "@/helpers/arrayUtils";

const { rt, tm } = useI18n();

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

const options = computed<SelectOption[]>(() =>
  orderBy(
    Object.entries(tm(rt("users.gender.options"))).map(([value, text]) => ({ text, value } as SelectOption)),
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
