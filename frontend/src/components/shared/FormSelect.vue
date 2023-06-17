<script setup lang="ts">
import { computed } from "vue";
import { useField } from "vee-validate";
import { useI18n } from "vue-i18n";
import type { SelectOption } from "@/types/components";
import type { ValidationListeners, ValidationRules } from "@/types/validation";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    id: string;
    label?: string;
    modelValue?: string;
    noLabel?: boolean;
    noState?: boolean;
    name?: string;
    options: SelectOption[];
    placeholder?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    noLabel: false,
    noState: false,
    required: false,
  }
);

const inputName = computed<string>(() => props.name ?? props.id);
const validationRules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.required) {
    rules.required = true;
  }
  return rules;
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
</script>

<template>
  <div class="mb-3">
    <label v-if="!noLabel && label" :for="id" class="form-label">
      <template v-if="required"><span class="text-danger">*</span></template> {{ t(label) }}
    </label>
    <slot name="before"></slot>
    <div class="input-group">
      <slot name="prepend"></slot>
      <select :class="classes" :disabled="disabled" :id="id" :name="inputName" :value="value" v-on="validationListeners">
        <option v-if="placeholder" :disabled="required" value="">{{ t(placeholder) }}</option>
        <option v-for="option in options" :key="option.value" :value="option.value">{{ option.text }}</option>
      </select>
      <slot name="append"></slot>
    </div>
    <slot name="after"></slot>
    <div v-if="errorMessage" class="invalid-feedback">{{ errorMessage }}</div>
  </div>
</template>
