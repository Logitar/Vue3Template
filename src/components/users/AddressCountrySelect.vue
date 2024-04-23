<script setup lang="ts">
import type { SelectOption } from "logitar-vue3-ui";
import { arrayUtils } from "logitar-js";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import AppSelect from "@/components/shared/AppSelect.vue";
import countries from "@/resources/countries.json";
import type { CountrySettings } from "@/types/settings";

const { orderBy } = arrayUtils;
const { t } = useI18n();

defineProps<{
  modelValue?: string;
  required?: boolean | string;
}>();

const options = computed<SelectOption[]>(() =>
  orderBy(
    countries.map(({ code }) => ({ text: t(`countries.${code}.name`), value: code })),
    "text",
  ),
);

const emit = defineEmits<{
  (e: "selected", value?: CountrySettings): void;
  (e: "update:model-value", value?: string): void;
}>();

function onModelValueUpdate(code?: string): void {
  emit("update:model-value", code);
  if (code) {
    const country = countries.find((country) => country.code === code);
    emit("selected", country);
  } else {
    emit("selected");
  }
}
</script>

<template>
  <AppSelect
    floating
    id="address-country"
    label="users.address.country.label"
    :model-value="modelValue"
    :options="options"
    placeholder="users.address.country.placeholder"
    :required="required"
    @update:model-value="onModelValueUpdate"
  />
</template>
