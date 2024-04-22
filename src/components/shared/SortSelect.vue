<script setup lang="ts">
import { TarCheckbox, TarSelect, type SelectOption } from "logitar-vue3-ui";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

withDefaults(
  defineProps<{
    descending?: boolean | string;
    id?: string;
    modelValue?: string;
    options?: SelectOption[];
  }>(),
  {
    id: "sort",
  },
);

defineEmits<{
  (e: "descending", value: boolean): void;
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <TarSelect
    floating
    :id="id"
    :label="t('sort.select.label')"
    :model-value="modelValue"
    :options="options"
    :placeholder="t('sort.select.placeholder')"
    @update:model-value="$emit('update:model-value', $event)"
  >
    <template #after>
      <TarCheckbox :id="`${id}_desc`" :label="t('sort.isDescending')" :model-value="descending" @update:model-value="$emit('descending', $event)" />
    </template>
  </TarSelect>
</template>
