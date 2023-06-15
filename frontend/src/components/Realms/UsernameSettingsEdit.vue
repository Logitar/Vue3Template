<script setup lang="ts">
import type { UsernameSettings } from "@/types/UsernameSettings";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const props = defineProps<{
  modelValue?: UsernameSettings;
}>();

const emit = defineEmits<{
  (e: "update:modelValue", value: UsernameSettings): void;
}>();
function onUpdate(changes: object): void {
  const settings: any = { ...props.modelValue };
  for (const [key, value] of Object.entries(changes)) {
    settings[key] = value;
  }
  emit("update:modelValue", settings);
}
</script>

<template>
  <div>
    <h5>{{ t("realms.username.title") }}</h5>
    <form-input
      id="allowedCharacters"
      label="realms.username.allowedCharacters.label"
      placeholder="realms.username.allowedCharacters.placeholder"
      :modelValue="modelValue?.allowedCharacters"
      @update:modelValue="onUpdate({ allowedCharacters: $event })"
    />
  </div>
</template>
