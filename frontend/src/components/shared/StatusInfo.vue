<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";
import type { Actor } from "@/types/Actor";
import { urlCombine } from "@/helpers/stringUtils";

const portalBaseUrl: string = import.meta.env.VITE_APP_PORTAL_BASE_URL;

const { d, t } = useI18n();

const props = defineProps<{
  actor: Actor;
  date: string;
  format: string;
}>();

const href = computed<string | undefined>(() => {
  const { id, isDeleted, type } = props.actor;
  if (!isDeleted) {
    switch (type) {
      case "ApiKey":
        return urlCombine(portalBaseUrl, `/api-keys/${id}`);
      case "User":
        return urlCombine(portalBaseUrl, `/users/${id}`);
    }
  }
  return undefined;
});
const icon = computed<string | undefined>(() => {
  switch (props.actor.type) {
    case "ApiKey":
      return "key";
    case "System":
      return "robot";
    case "User":
      return "user";
  }
  return undefined;
});
const variant = computed<string | undefined>(() => (props.actor.type === "ApiKey" ? "info" : undefined));
</script>

<template>
  <span>
    {{ t(format, { date: d(date, "medium") }) }}
    <template v-if="actor.isDeleted">
      <app-avatar :display-name="actor.displayName" :email-address="actor.emailAddress" :icon="icon" :size="24" :url="actor.picture" :variant="variant" />
      {{ actor.displayName }}
    </template>
    <a v-else :href="href" target="_blank">
      <app-avatar :display-name="actor.displayName" :email-address="actor.emailAddress" :icon="icon" :size="24" :url="actor.picture" :variant="variant" />
      {{ actor.displayName }} <font-awesome-icon icon="fas fa-arrow-up-right-from-square" />
    </a>
  </span>
</template>
