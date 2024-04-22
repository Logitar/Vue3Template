<script setup lang="ts">
import type { SelectOption } from "logitar-vue3-ui";
import { computed } from "vue";

import AppSelect from "@/components/shared/AppSelect.vue";
import timeZones from "@/resources/timeZones.json";
import { orderBy } from "@/helpers/arrayUtils";

defineProps<{
  modelValue?: string;
}>();

const options = computed<SelectOption[]>(() =>
  orderBy(
    timeZones.map(({ id, displayName }) => ({ value: id, text: displayName })),
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
    id="time-zone"
    label="users.timeZone.label"
    :model-value="modelValue"
    :options="options"
    placeholder="users.timeZone.placeholder"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
