<script setup lang="ts">
import { useI18n } from "vue-i18n";
import type { UsernameSettings } from "@/types/realms";
import { assign } from "@/helpers/objectUtils";

const { t } = useI18n();

const props = defineProps<{
  modelValue?: UsernameSettings;
}>();

const emit = defineEmits<{
  (e: "update:model-value", value: UsernameSettings): void;
}>();
function onUpdate(changes: object): void {
  const settings: UsernameSettings = { ...props.modelValue };
  for (const [key, value] of Object.entries(changes)) {
    assign(settings, key as keyof UsernameSettings, value);
  }
  emit("update:model-value", settings);
}
</script>

<template>
  <div>
    <h5>{{ t("realms.username.title") }}</h5>
    <form-input
      id="allowedCharacters"
      label="realms.username.allowedCharacters.label"
      placeholder="realms.username.allowedCharacters.placeholder"
      :model-value="modelValue?.allowedCharacters"
      @update:model-value="onUpdate({ allowedCharacters: $event })"
    />
  </div>
</template>
