<script setup lang="ts">
import { computed } from "vue";
import type { AlertVariant } from "@/types/AlertVariant";

const props = withDefaults(
  defineProps<{
    dismissible?: boolean;
    modelValue?: boolean;
    show?: boolean;
    variant?: AlertVariant;
  }>(),
  {
    dismissible: false,
    modelValue: false,
    show: false,
    variant: "danger",
  }
);

const isShown = computed<boolean>(() => props.show || props.modelValue);
const classes = computed<string[]>(() => {
  const classes = ["alert", "fade"];
  if (props.variant) {
    classes.push(`alert-${props.variant}`);
  }
  if (props.dismissible) {
    classes.push("alert-dismissible");
  }
  if (isShown.value) {
    classes.push("show");
  }
  return classes;
});

const emit = defineEmits(["update:modelValue"]);
function onClose() {
  emit("update:modelValue", false);
}
</script>

<template>
  <div :class="classes" role="alert" v-show="isShown">
    <slot></slot>
    <button v-if="dismissible" type="button" class="btn-close" aria-label="Close" @click="onClose"></button>
  </div>
</template>
