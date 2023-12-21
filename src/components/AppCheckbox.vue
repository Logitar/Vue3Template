<script setup lang="ts">
import { computed, ref } from "vue";
import { nanoid } from "nanoid";

import type { CheckboxOptions } from "./AppCheckbox";

const props = defineProps<CheckboxOptions>();

const inputRef = ref<HTMLInputElement>();

const classes = computed<string[]>(() => {
  const classes = ["form-check"];
  if (props.inline) {
    classes.push("form-check-inline");
  }
  if (props.reverse) {
    classes.push("form-check-reverse");
  }
  if (props.switch) {
    classes.push("form-switch");
  }
  return classes;
});
const inputId = computed<string>(() => props.id ?? nanoid());
const role = computed<string | undefined>(() => (props.switch ? "switch" : undefined));

const emit = defineEmits<{
  (e: "update:model-value", value: boolean): void;
}>();
function onChange(event: Event): void {
  emit("update:model-value", (event.target as HTMLInputElement).checked);
}

function focus(): void {
  inputRef.value?.focus();
}
defineExpose({ focus });
</script>

<template>
  <div :class="classes">
    <slot>
      <input
        :aria-label="ariaLabel"
        :checked="modelValue"
        class="form-check-input"
        :disabled="disabled"
        :id="inputId"
        :name="name"
        ref="inputRef"
        :required="required"
        :role="role"
        type="checkbox"
        :value="value"
        @change="onChange"
      />
      <slot name="label-override">
        <label v-if="label" class="form-check-label" :for="inputId">{{ label }}</label>
      </slot>
    </slot>
  </div>
</template>
