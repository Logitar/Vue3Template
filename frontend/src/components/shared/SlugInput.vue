<script setup lang="ts">
import { ref, watch } from "vue";
import { slugify } from "@/helpers/stringUtils";

const props = withDefaults(
  defineProps<{
    baseValue?: string;
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    name?: string;
    placeholder?: string;
    required?: boolean;
    validate?: boolean;
  }>(),
  {
    disabled: false,
    id: "slug",
    label: "slug.label",
    placeholder: "slug.placeholder",
    required: false,
    validate: false,
  }
);

const isCustom = ref<boolean>(false);

const emit = defineEmits<{
  (e: "update:modelValue", value: string): void;
}>();
function onCustomUpdate(value: boolean): void {
  isCustom.value = value;
  if (!value) {
    emit("update:modelValue", slugify(props.baseValue));
  }
}
function onModelValueUpdate(value: string): void {
  emit("update:modelValue", value);
}

watch(
  () => props.baseValue,
  (value) => {
    if (!props.disabled && !isCustom.value) {
      emit("update:modelValue", slugify(value));
    }
  },
  { immediate: true }
);
</script>

<template>
  <form-input
    :disabled="disabled || !isCustom"
    :id="id"
    :label="label"
    :maxLength="validate ? 255 : undefined"
    :modelValue="modelValue"
    :name="name"
    :placeholder="placeholder"
    :required="required"
    :rules="{ slug: true }"
    @update:modelValue="onModelValueUpdate"
  >
    <template #after>
      <form-checkbox v-if="!disabled" :id="`${id}_custom`" label="custom" :modelValue="isCustom" @update:modelValue="onCustomUpdate" />
    </template>
  </form-input>
</template>
