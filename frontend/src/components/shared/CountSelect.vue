<script setup lang="ts">
import { computed } from "vue";
import type { SelectOption } from "@/types/SelectOption";

withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: number;
    name?: string;
  }>(),
  {
    disabled: false,
    id: "count",
    label: "count",
    modelValue: 10,
  }
);

const options = computed<SelectOption[]>(() => [10, 25, 50, 100].map((value) => ({ text: value.toString(), value: value.toString() })));

const emit = defineEmits<{
  (e: "update:model-value", value: number): void;
}>();
function onModelValueUpdate(value: string): void {
  emit("update:model-value", Number(value));
}
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :model-value="modelValue.toString()"
    :name="name"
    no-state
    :options="options"
    @update:model-value="onModelValueUpdate"
  />
</template>
