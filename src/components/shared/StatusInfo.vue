<script setup lang="ts">
import { TarAvatar } from "logitar-vue3-ui";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import type { Actor } from "@/types/actor";

const { d, t } = useI18n();

const props = defineProps<{
  actor: Actor;
  date: string;
  format: string;
}>();

const displayName = computed<string>(() => {
  const { displayName, type } = props.actor;
  return type === "System" ? t("system") : displayName;
});
const icon = computed<string | undefined>(() => {
  switch (props.actor.type) {
    case "ApiKey":
      return "fas fa-key";
    case "System":
      return "fas fa-robot";
    case "User":
      return "fas fa-user";
  }
  return undefined;
});
const variant = computed<string | undefined>(() => (props.actor.type === "ApiKey" ? "info" : undefined));
</script>

<template>
  <span>
    {{ t(format, { date: d(date, "medium") }) }}
    <TarAvatar :display-name="displayName" :email-address="actor.emailAddress" :icon="icon" :size="24" :url="actor.pictureUrl" :variant="variant" />
    {{ displayName }}
  </span>
</template>
