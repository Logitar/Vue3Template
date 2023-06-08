<script setup lang="ts">
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    active?: boolean;
    disabled?: boolean;
    id: string;
    target: string;
  }>(),
  {
    active: false,
    disabled: false,
  }
);

const classes = computed<string[]>(() => {
  const classes = ["nav-link"];
  if (props.active) {
    classes.push("active");
  }
  return classes;
});
</script>

<template>
  <li class="nav-item" role="presentation">
    <button
      :class="classes"
      :id="id"
      data-bs-toggle="tab"
      :data-bs-target="`#${target}`"
      type="button"
      role="tab"
      :aria-controls="target"
      aria-selected="true"
      :disabled="disabled"
    >
      <slot></slot>
    </button>
  </li>
</template>
