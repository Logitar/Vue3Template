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
  (e: "update:modelValue", date?: Date): void;
}>();
function onModelValueUpdate(value: string): void {
  try {
    const date = new Date(value);
    emit("update:modelValue", isNaN(Number(date)) ? undefined : date);
  } catch {
    emit("update:modelValue", undefined);
  }
}
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :maxDate="formattedMax"
    :minDate="formattedMin"
    :modelValue="formattedValue"
    :name="name"
    :required="required"
    type="datetime-local"
    @update:modelValue="onModelValueUpdate"
  />
</template>
