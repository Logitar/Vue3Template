<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();

defineProps<{
  id: string;
  label?: string;
  modelValue?: boolean;
}>();

const emit = defineEmits<{
  (e: "update:modelValue", value: boolean): void;
}>();
function onChange($event: Event) {
  if ($event.target) {
    emit("update:modelValue", ($event.target as HTMLInputElement).checked);
  }
}
</script>

<template>
  <div class="form-check">
    <input type="checkbox" :checked="modelValue" class="form-check-input" :id="id" @change="onChange" />
    <label class="form-check-label" :for="id">
      <template v-if="label">{{ t(label) }}</template>
      <slot name="label"></slot>
    </label>
  </div>
</template>
