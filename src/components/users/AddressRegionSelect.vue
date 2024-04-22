<script setup lang="ts">
import type { SelectOption } from "logitar-vue3-ui";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import AppSelect from "@/components/shared/AppSelect.vue";
import type { CountrySettings } from "@/types/settings";
import { orderBy } from "@/helpers/arrayUtils";

const { t } = useI18n();

const props = defineProps<{
  country?: CountrySettings;
  modelValue?: string;
  required?: boolean | string;
}>();

const options = computed<SelectOption[]>(() =>
  orderBy(props.country?.regions.map((region) => ({ text: t(`countries.${props.country?.code}.regions.${region}`), value: region })) ?? [], "text"),
);

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppSelect
    floating
    id="address-region"
    label="users.address.region.label"
    :model-value="modelValue"
    :options="options"
    placeholder="users.address.region.placeholder"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
