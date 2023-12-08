<script setup lang="ts">
import { computed } from "vue";
import md5 from "md5";
import type { BadgeVariant } from "@/types/components";

const props = withDefaults(
  defineProps<{
    displayName: string;
    emailAddress?: string;
    icon?: string;
    size?: number;
    url?: string;
    variant?: BadgeVariant;
  }>(),
  {
    icon: "fas fa-user",
    size: 32,
    variant: "dark",
  }
);

const alt = computed<string>(() => `${props.displayName}'s Avatar'`);
const src = computed<string | undefined>(() => {
  if (props.url) {
    return props.url;
  } else if (props.emailAddress) {
    return `https://www.gravatar.com/avatar/${md5(props.emailAddress)}`;
  }
  return undefined;
});
</script>

<template>
  <span>
    <img v-if="src" class="rounded-circle" :src="src" :alt="alt" :height="size" />
    <app-badge v-else class="rounded-circle" :style="{ width: `${size}px`, height: `${size}px` }" :variant="variant">
      <font-awesome-icon :icon="icon" />
    </app-badge>
  </span>
</template>

<style scoped>
.badge {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  vertical-align: middle;
  flex-shrink: 0;
  font-size: inherit;
  font-weight: 400;
  line-height: 1;
  max-width: 100%;
  text-align: center;
  overflow: visible;
  position: relative;
  transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}
</style>
