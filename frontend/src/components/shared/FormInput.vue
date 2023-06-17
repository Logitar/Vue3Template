<script setup lang="ts">
import { computed, ref } from "vue";
import { useField } from "vee-validate";
import { useI18n } from "vue-i18n";
import type { ValidationListeners } from "@/types/ValidationListeners";
import type { ValidationRules } from "@/types/ValidationRules";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    id: string;
    label?: string;
    maxDate?: string;
    maxLength?: number;
    maxValue?: number;
    minDate?: string;
    minLength?: number;
    minValue?: number;
    modelValue?: string;
    name?: string;
    noLabel?: boolean;
    noState?: boolean;
    placeholder?: string;
    required?: boolean;
    rules?: object;
    step?: number;
    type?: string;
  }>(),
  {
    disabled: false,
    noLabel: false,
    noState: false,
    required: false,
    type: "text",
  }
);

const inputMax = computed<string | number | undefined>(() => {
  switch (props.type) {
    case "datetime-local":
      return props.maxDate;
    case "number":
      return props.maxValue;
  }
  return undefined;
});
const inputMin = computed<string | number | undefined>(() => {
  switch (props.type) {
    case "datetime-local":
      return props.minDate;
    case "number":
      return props.minValue;
  }
  return undefined;
});
const inputName = computed<string>(() => props.name ?? props.id);
const validationRules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.required) {
    rules.required = true;
  }

  if (props.type !== "datetime-local") {
    if (props.type === "number") {
      if (props.maxValue) {
        rules.max_value = props.maxValue;
      }
      if (props.minValue) {
        rules.min_value = props.minValue;
      }
    } else {
      if (props.maxLength) {
        rules.max_length = props.maxLength;
      }
      if (props.minLength) {
        rules.min_length = props.minLength;
      }
    }
  }

  if (props.type === "email" || props.type === "url") {
    rules[props.type] = true;
  }

  return { ...rules, ...props.rules };
});

const { errorMessage, handleChange, meta, value } = useField<string>(inputName, validationRules, {
  initialValue: props.modelValue || undefined,
  label: props.label ? t(props.label).toLowerCase() : inputName,
});
const classes = computed<string[]>(() => {
  const classes = ["form-control"];
  if (!props.noState && (meta.dirty || meta.touched)) {
    classes.push(meta.valid ? "is-valid" : "is-invalid");
  }
  return classes;
});
const validationListeners = computed<ValidationListeners>(() => ({
  blur: handleChange,
  change: handleChange,
  input: errorMessage.value ? handleChange : (e: unknown) => handleChange(e, false),
}));

const inputRef = ref<HTMLInputElement>();
function focus(): void {
  inputRef.value?.focus();
}
defineExpose({ focus });
</script>

<template>
  <div class="mb-3">
    <label v-if="!noLabel && label" :for="id" class="form-label">
      <template v-if="required"><span class="text-danger">*</span></template> {{ t(label) }}
    </label>
    <slot name="before"></slot>
    <div class="input-group">
      <slot name="prepend"></slot>
      <input
        :class="classes"
        :disabled="disabled"
        :id="id"
        :max="inputMax"
        :min="inputMin"
        :name="inputName"
        :placeholder="placeholder ? t(placeholder) : undefined"
        ref="inputRef"
        :step="type === 'number' ? step : undefined"
        :type="type"
        :value="value"
        v-on="validationListeners"
      />
      <slot name="append"></slot>
    </div>
    <slot name="after"></slot>
    <div v-if="errorMessage" class="invalid-feedback">{{ errorMessage }}</div>
  </div>
</template>
