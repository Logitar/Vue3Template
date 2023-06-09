<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import type { CountrySettings } from "@/types/CountrySettings";
import type { SelectOption } from "@/types/SelectOption";
import { orderBy } from "@/helpers/arrayUtils";

const { t } = useI18n();

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

const options = computed<SelectOption[]>(() =>
  orderBy(props.country?.regions.map((region) => ({ text: t(`countries.${props.country?.code}.regions.${region}`), value: region })) ?? [], "text")
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
