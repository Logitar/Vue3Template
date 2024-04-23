<script setup lang="ts">
import { TarPagination, type PaginationPosition } from "logitar-vue3-ui";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

withDefaults(
  defineProps<{
    ariaFirst?: string;
    ariaLabel?: string;
    ariaLast?: string;
    ariaNext?: string;
    ariaPrevious?: string;
    count?: number;
    modelValue?: number;
    pages?: number;
    position?: PaginationPosition;
    total?: number;
  }>(),
  {
    ariaFirst: "pagination.first",
    ariaLabel: "pagination.label",
    ariaLast: "pagination.last",
    ariaNext: "pagination.next",
    ariaPrevious: "pagination.previous",
    pages: 5,
    position: "center",
  },
);

defineEmits<{
  (e: "update:model-value", value: number): void;
}>();
</script>

<template>
  <TarPagination
    :aria-first="t(ariaFirst)"
    :aria-label="t(ariaLabel)"
    :aria-last="t(ariaLast)"
    :aria-next="t(ariaNext)"
    :aria-previous="t(ariaPrevious)"
    :count="count"
    first="«"
    last="»"
    :model-value="modelValue"
    next="›"
    :pages="pages"
    position="center"
    previous="‹"
    :total="total"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
