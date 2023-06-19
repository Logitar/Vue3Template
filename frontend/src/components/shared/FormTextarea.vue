<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import { useField } from "vee-validate";
import type { ValidationListeners, ValidationRules } from "@/types/validation";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    cols?: number;
    disabled?: boolean;
    id: string;
    label?: string;
    maxLength?: number;
    minLength?: number;
    modelValue?: string;
    name?: string;
    placeholder?: string;
    required?: boolean;
    rows?: number;
    rules?: object;
  }>(),
  {
    cols: 20,
    disabled: false,
    required: false,
    rows: 2,
  }
);

const displayLabel = computed<string>(() => (props.label ? t(props.label).toLowerCase() : inputName.value));
const inputName = computed<string>(() => props.name ?? props.id);
const validationRules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.required) {
    rules.required = true;
  }

  if (props.maxLength) {
    rules.max_length = props.maxLength;
  }
  if (props.minLength) {
    rules.min_length = props.minLength;
  }

  return { ...rules, ...props.rules };
});

const { errorMessage, handleChange, meta, value } = useField<string>(inputName, validationRules, {
  initialValue: props.modelValue || undefined,
  label: displayLabel,
});
const classes = computed<string[]>(() => {
  const classes = ["form-control"];
  if (meta.dirty || meta.touched) {
    classes.push(meta.valid ? "is-valid" : "is-invalid");
  }
  return classes;
});
const validationListeners = computed<ValidationListeners>(() => ({
  blur: handleChange,
  change: handleChange,
  input: errorMessage.value ? handleChange : (e: unknown) => handleChange(e, false),
}));
</script>

<template>
  <div class="mb-3">
    <label v-if="label" :for="id" class="form-label">
      <template v-if="required"><span class="text-danger">*</span></template> {{ t(label) }}
    </label>
    <slot name="before"></slot>
    <div class="input-group">
      <slot name="prepend"></slot>
      <textarea
        :class="classes"
        :cols="cols"
        :disabled="disabled"
        :id="id"
        :name="inputName"
        :placeholder="placeholder ? t(placeholder) : undefined"
        :rows="rows"
        :value="value"
        v-on="validationListeners"
      ></textarea>
      <slot name="append"></slot>
    </div>
    <slot name="after"></slot>
    <div v-if="errorMessage" class="invalid-feedback">{{ errorMessage }}</div>
  </div>
</template>
