<script setup lang="ts">
import { computed, watchEffect } from "vue";
import { useI18n } from "vue-i18n";
import countries from "@/resources/countries.json";
import type { CountrySettings } from "@/types/CountrySettings";
import type { SelectOption } from "@/types/SelectOption";
import { orderBy } from "@/helpers/arrayUtils";

const { t } = useI18n();

const props = withDefaults(
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

const options = computed<SelectOption[]>(() =>
  orderBy(
    countries.map(({ code }) => ({ text: t(`countries.${code}.name`), value: code })),
    "text"
  )
);

const map = new Map<string, CountrySettings>();
countries.forEach((country) => map.set(country.code, country));
const emit = defineEmits(["countrySelected", "update:modelValue"]);
watchEffect(() => {
  if (props.modelValue) {
    const country: CountrySettings | undefined = map.get(props.modelValue);
    emit("countrySelected", country);
  } else {
    emit("countrySelected", undefined);
  }
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
