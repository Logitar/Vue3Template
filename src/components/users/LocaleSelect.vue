<script setup lang="ts">
import type { SelectOption } from "logitar-vue3-ui";
import { computed } from "vue";

import AppSelect from "@/components/shared/AppSelect.vue";
import locales from "@/resources/locales.json";
import { orderBy } from "@/helpers/arrayUtils";

defineProps<{
  modelValue?: string;
}>();

const options = computed<SelectOption[]>(() =>
  orderBy(
    locales.map(({ code, nativeName }) => ({ value: code, text: nativeName })),
    "text",
  ),
);

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppSelect
    floating
    id="locale"
    label="users.locale.label"
    :model-value="modelValue"
    :options="options"
    placeholder="users.locale.placeholder"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
