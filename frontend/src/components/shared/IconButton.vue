<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import { useRouter, type RouteLocationRaw } from "vue-router";
import type { ButtonType } from "@/types/ButtonType";
import type { ButtonVariant } from "@/types/ButtonVariant";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    disabled?: boolean;
    icon: string;
    loading?: boolean;
    text?: string;
    to?: RouteLocationRaw;
    type?: ButtonType;
    variant?: ButtonVariant;
  }>(),
  {
    disabled: false,
    loading: false,
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

const router = useRouter();
function onClick(): void {
  if (props.to) {
    router.push(props.to);
  }
}
</script>

<template>
  <button :class="classes" :disabled="disabled" :type="type" @click="onClick">
    <div v-if="loading" class="spinner-border spinner-border-sm" role="status">
      <span class="visually-hidden">{{ t("loading") }}</span>
    </div>
    <font-awesome-icon v-else :icon="icon" />
    <template v-if="text">&nbsp;{{ t(text) }}</template>
    <slot></slot>
  </button>
</template>
