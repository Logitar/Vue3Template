<script setup lang="ts">
import type { SelectOption } from "logitar-vue3-ui";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import AppSelect from "@/components/shared/AppSelect.vue";
import { orderBy } from "@/helpers/arrayUtils";

const { rt, tm } = useI18n();

defineProps<{
  modelValue?: string;
}>();

const options = computed<SelectOption[]>(() =>
  orderBy(
    Object.entries(tm(rt("users.gender.options"))).map(([value, text]) => ({ text, value }) as SelectOption),
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
    id="gender"
    label="users.gender.label"
    :model-value="modelValue"
    :options="options"
    placeholder="users.gender.placeholder"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
