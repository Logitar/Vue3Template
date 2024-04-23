<script setup lang="ts">
import { TarSelect, parsingUtils, type SelectOption } from "logitar-vue3-ui";
import { ref } from "vue";
import { useI18n } from "vue-i18n";

const { parseNumber } = parsingUtils;
const { t } = useI18n();

defineProps<{
  modelValue?: number;
}>();

const options = ref<SelectOption[]>([{ text: "10" }, { text: "25" }, { text: "50" }, { text: "100" }]);

defineEmits<{
  (e: "update:model-value", value?: number): void;
}>();
</script>

<template>
  <TarSelect
    floating
    id="count"
    :label="t('count')"
    :model-value="modelValue?.toString()"
    :options="options"
    @update:model-value="$emit('update:model-value', parseNumber($event))"
  />
</template>
