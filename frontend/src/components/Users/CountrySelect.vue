<script setup lang="ts">
import { computed } from "vue";
import countries from "@/resources/countries.json";
import type { CountrySettings } from "@/types/CountrySettings";
import type { SelectOption } from "@/types/SelectOption";

const map = new Map<string, CountrySettings>();
countries.forEach((country) => map.set(country.code, country));

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
    id: "country",
    label: "users.address.country.label",
    placeholder: "users.address.country.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() => countries.map(({ code }) => ({ key: `countries.${code}.name`, value: code }))); // TODO(fpion): sort

const emit = defineEmits(["countrySelected", "update:modelValue"]);
function onModelValueUpdated(value: string): void {
  emit("update:modelValue", value);
  if (value) {
    const country: CountrySettings | undefined = map.get(value);
    emit("countrySelected", country);
  } else {
    emit("countrySelected", undefined);
  }
}
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
    @update:modelValue="onModelValueUpdated"
  />
</template>
