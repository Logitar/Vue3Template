<script setup lang="ts">
import { computed } from "vue";
import type { CountrySettings } from "@/types/CountrySettings";
import type { SelectOption } from "@/types/SelectOption";

const props = withDefaults(
  defineProps<{
    country?: CountrySettings;
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    placeholder?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    id: "region",
    label: "users.address.region.label",
    placeholder: "users.address.region.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(
  () => props.country?.regions.map((region) => ({ key: `countries.${props.country?.code}.regions.${region}`, value: region })) ?? []
); // TODO(fpion): sort
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
