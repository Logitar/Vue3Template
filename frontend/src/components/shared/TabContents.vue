<script setup lang="ts">
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    active?: boolean;
    header: string;
    id: string;
  }>(),
  {
    active: false,
  }
);

const classes = computed<string[]>(() => {
  const classes = ["tab-pane", "fade"];
  if (props.active) {
    classes.push("show");
    classes.push("active");
  }
  return classes;
});
</script>

<template>
  <div :class="classes" :id="id" role="tabpanel" :aria-labelledby="header" tabindex="0">
    <slot></slot>
  </div>
</template>
