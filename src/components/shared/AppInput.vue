<script setup lang="ts">
import { TarInput, inputUtils, parsingUtils, type InputOptions, type InputStatus } from "logitar-vue3-ui";
import { computed, ref } from "vue";
import { nanoid } from "nanoid";
import { useField } from "vee-validate";
import { useI18n } from "vue-i18n";

import type { ShowStatus, ValidationListeners, ValidationRules, ValidationType } from "@/types/validation";
import { isNullOrWhiteSpace } from "@/helpers/stringUtils";

const { isDateTimeInput, isNumericInput, isTextualInput } = inputUtils;
const { parseBoolean, parseNumber } = parsingUtils;
const { t } = useI18n();

const props = withDefaults(
  defineProps<
    InputOptions & {
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

const inputRef = ref<InstanceType<typeof TarInput> | null>(null);

const describedBy = computed<string>(() => `${props.id}_invalid-feedback`);
const inputMax = computed<number | string | undefined>(() => (props.validation === "server" || isDateTimeInput(props.type) ? props.max : undefined));
const inputMin = computed<number | string | undefined>(() => (props.validation === "server" || isDateTimeInput(props.type) ? props.min : undefined));
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
  const min: number | undefined = parseNumber(props.min);
  if (isNumericInput(props.type)) {
    if (max) {
      rules.max_value = max;
    }
    if (min) {
      rules.min_value = min;
    }
  } else if (isTextualInput(props.type)) {
    if (max) {
      rules.max_length = max;
    }
    if (min) {
      rules.min_length = min;
    }
  }

  if (!isNullOrWhiteSpace(props.pattern)) {
    rules.regex = props.pattern;
  }

  switch (props.type) {
    case "email":
      rules.email = true;
      break;
    case "url":
      rules.url = true;
      break;
  }

  return { ...rules, ...props.rules };
});
const displayLabel = computed<string>(() => (props.label ? t(props.label).toLowerCase() : inputName.value));
const { errorMessage, handleChange, meta, value } = useField<string>(inputName, validationRules, {
  initialValue: props.modelValue,
  label: displayLabel,
});
const inputStatus = computed<InputStatus | undefined>(() => {
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
  inputRef.value?.focus();
}
defineExpose({ focus });
</script>

<template>
  <TarInput
    :described-by="describedBy"
    :disabled="disabled"
    :floating="floating"
    :id="id"
    :label="label ? t(label) : undefined"
    :max="inputMax"
    :min="inputMin"
    :model-value="validation === 'server' ? modelValue : value"
    :name="name"
    :pattern="validation === 'server' ? pattern : undefined"
    :placeholder="placeholder ? t(placeholder) : undefined"
    :plaintext="plaintext"
    :readonly="readonly"
    ref="inputRef"
    :required="inputRequired"
    :size="size"
    :status="inputStatus"
    :step="step"
    :type="type"
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
  </TarInput>
</template>
