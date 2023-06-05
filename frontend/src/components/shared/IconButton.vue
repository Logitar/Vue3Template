<script setup lang="ts">
import { computed } from "vue";

type ButtonType = "button" | "submit" | "reset" | undefined;
type ButtonVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark" | "link" | undefined;

const props = withDefaults(
  defineProps<{
    icon: string;
    type?: ButtonType;
    variant?: ButtonVariant;
  }>(),
  {
    type: "button",
    variant: "primary",
  }
);

const classes = computed<string[]>(() => {
  const classes = ["btn"];
  if (props.variant) {
    classes.push(`btn-${props.variant}`);
  }
  return classes;
});
</script>

<template>
  <button :class="classes" :type="type">
    <font-awesome-icon :icon="icon" />
    <slot></slot>
  </button>
</template>
