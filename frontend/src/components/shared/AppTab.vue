<script setup lang="ts">
import { computed, inject, onMounted, onUnmounted } from "vue";
import { bindTabKey, unbindTabKey } from "@/inject/AppTabs";
import type { TabOptions } from "@/types/TabOptions";

const bindTab: ((tab: TabOptions) => void) | undefined = inject(bindTabKey);
const unbindTab: ((tab: TabOptions) => void) | undefined = inject(unbindTabKey);

const props = withDefaults(
  defineProps<{
    active?: boolean;
    disabled?: boolean;
    id: string; // TODO(fpion): remove
    title: string;
  }>(),
  {
    active: false,
    disabled: false,
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

const options = computed<TabOptions>(() => ({ active: props.active, disabled: props.disabled, id: props.id, title: props.title }));
onMounted(() => {
  if (bindTab) {
    bindTab(options.value);
  }
});
onUnmounted(() => {
  if (unbindTab) {
    unbindTab(options.value);
  }
});
</script>

<template>
  <div :class="classes" :id="`${props.id}_pane`" role="tabpanel" :aria-labelledby="`${props.id}_head`" tabindex="0">
    <slot></slot>
  </div>
</template>
