<script setup lang="ts">
import { computed } from "vue";
import type { BadgeVariant } from "@/types/BadgeVariant";

const props = withDefaults(
  defineProps<{
    variant?: BadgeVariant;
  }>(),
  {
    variant: "info",
  }
);

const classes = computed<string[]>(() => {
  const classes = ["badge"];
  if (props.variant) {
    classes.push(`bg-${props.variant}`);
  }
  return classes;
});
</script>

<template>
  <span :class="classes">
    <slot></slot>
  </span>
</template>
