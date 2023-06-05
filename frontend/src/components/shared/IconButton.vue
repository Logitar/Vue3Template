<script setup lang="ts">
import { type PropType } from "vue";
import { computed } from "vue";

type ButtonType = "button" | "submit" | "reset" | undefined;
type ButtonVariant = "primary" | "secondary" | "success" | "danger" | "warning" | "info" | "light" | "dark" | "link" | undefined;

const props = defineProps({
  icon: {
    type: String,
    required: true,
  },
  type: {
    type: String as PropType<ButtonType>,
    default: "button",
  },
  variant: {
    type: String as PropType<ButtonVariant>,
    default: "primary",
  },
});

const classes = computed(() => {
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
