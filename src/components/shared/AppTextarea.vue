<script setup lang="ts">
import { TarTextarea, parsingUtils, type TextareaOptions, type TextareaStatus } from "logitar-vue3-ui";
import { computed, ref } from "vue";
import { nanoid } from "nanoid";
import { useField } from "vee-validate";
import { useI18n } from "vue-i18n";

import type { ShowStatus, ValidationListeners, ValidationRules, ValidationType } from "@/types/validation";

const { parseBoolean, parseNumber } = parsingUtils;
const { t } = useI18n();

const props = withDefaults(
  defineProps<
    TextareaOptions & {
      rules?: ValidationRules;
      showStatus?: ShowStatus;
      validation?: ValidationType;
    }
  >(),
  {
    id: () => nanoid(),
    showStatus: "touched",
    validation: "client",
  },
);

const textareaRef = ref<InstanceType<typeof TarTextarea> | null>(null);

const describedBy = computed<string>(() => `${props.id}_invalid-feedback`);
const inputName = computed<string>(() => props.name ?? props.id);
const inputRequired = computed<boolean | "label">(() => (parseBoolean(props.required) ? (props.validation === "server" ? true : "label") : false));

const validationRules = computed<ValidationRules>(() => {
  const rules: ValidationRules = {};
  if (props.validation === "server") {
    return rules;
  }

  const required: boolean | undefined = parseBoolean(props.required);
  if (required) {
    rules.required = true;
  }

  const max: number | undefined = parseNumber(props.max);
  if (max) {
    rules.max_length = max;
  }
  const min: number | undefined = parseNumber(props.min);
  if (min) {
    rules.min_length = min;
  }

  return { ...rules, ...props.rules };
});
const displayLabel = computed<string>(() => (props.label ? t(props.label).toLowerCase() : inputName.value));
const { errorMessage, handleChange, meta, value } = useField<string>(inputName, validationRules, {
  initialValue: props.modelValue,
  label: displayLabel,
});
const inputStatus = computed<TextareaStatus | undefined>(() => {
  if (props.showStatus === "always" || (props.showStatus === "touched" && (meta.dirty || meta.touched))) {
    return props.status ?? (props.validation === "server" ? undefined : meta.valid ? "valid" : "invalid");
  }
  return undefined;
});
const validationListeners = computed<ValidationListeners>(() => ({
  blur: handleChange,
  change: handleChange,
  input: errorMessage.value ? handleChange : (e: unknown) => handleChange(e, false),
}));

function focus(): void {
  textareaRef.value?.focus();
}
defineExpose({ focus });
</script>

<template>
  <TarTextarea
    :cols="cols"
    :described-by="describedBy"
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label ? t(label) : undefined"
    :max="validation === 'server' ? max : undefined"
    :min="validation === 'server' ? min : undefined"
    :model-value="validation === 'server' ? modelValue : value"
    :name="name"
    :placeholder="placeholder ? t(placeholder) : undefined"
    :plaintext="plaintext"
    :readonly="readonly"
    ref="textareaRef"
    :required="inputRequired"
    :rows="rows"
    :size="size"
    :status="inputStatus"
    v-on="validationListeners"
  >
    <template #before>
      <slot name="before"></slot>
    </template>
    <template #prepend>
      <slot name="prepend"></slot>
    </template>
    <template #append>
      <slot name="append"></slot>
    </template>
    <template #after>
      <div v-if="errorMessage" class="invalid-feedback" :id="describedBy">{{ errorMessage }}</div>
      <slot name="after"></slot>
    </template>
  </TarTextarea>
</template>
