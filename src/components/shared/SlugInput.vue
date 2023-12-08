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
  (e: "update:model-value", value: string): void;
}>();
function onCustomUpdate(value: boolean): void {
  isCustom.value = value;
  if (!value) {
    emit("update:model-value", slugify(props.baseValue));
  }
}
function onModelValueUpdate(value: string): void {
  emit("update:model-value", value);
}

watch(
  () => props.baseValue,
  (value) => {
    if (!props.disabled && !isCustom.value) {
      emit("update:model-value", slugify(value));
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
    :max-length="validate ? 255 : undefined"
    :model-value="modelValue"
    :name="name"
    :placeholder="placeholder"
    :required="required"
    :rules="{ slug: true }"
    @update:model-value="onModelValueUpdate"
  >
    <template #after>
      <form-checkbox v-if="!disabled" :id="`${id}_custom`" label="custom" :model-value="isCustom" @update:model-value="onCustomUpdate" />
    </template>
  </form-input>
</template>
