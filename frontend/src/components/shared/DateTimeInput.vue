<script setup lang="ts">
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    id: string;
    label?: string;
    max?: Date;
    min?: Date;
    modelValue?: Date;
    name?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    required: false,
  }
);

function getDateTimeLocal(value: Date): string {
  const date = [value.getFullYear(), (value.getMonth() + 1).toString().padStart(2, "0"), value.getDate().toString().padStart(2, "0")].join("-");
  const time = [value.getHours().toString().padStart(2, "0"), value.getMinutes().toString().padStart(2, "0")].join(":");
  return [date, time].join("T");
}
const formattedMax = computed<string | undefined>(() => (props.max ? getDateTimeLocal(props.max) : undefined));
const formattedMin = computed<string | undefined>(() => (props.min ? getDateTimeLocal(props.min) : undefined));
const formattedValue = computed<string | undefined>(() => (props.modelValue ? getDateTimeLocal(props.modelValue) : undefined));

const emit = defineEmits<{
  (e: "update:model-value", date?: Date): void;
}>();
function onModelValueUpdate(value: string): void {
  try {
    const date = new Date(value);
    emit("update:model-value", isNaN(date.getTime()) ? undefined : date);
  } catch {
    emit("update:model-value", undefined);
  }
}
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :max-date="formattedMax"
    :min-date="formattedMin"
    :model-value="formattedValue"
    :name="name"
    :required="required"
    type="datetime-local"
    @update:model-value="onModelValueUpdate"
  />
</template>
