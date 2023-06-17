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
}>();

const displayName = computed<string>(() => {
  const { displayName, type } = props.actor;
  return type === "System" ? t("system") : displayName;
});
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
  <div class="d-flex">
    <div class="d-flex">
      <div class="d-flex align-content-center flex-wrap mx-1">
        <a v-if="href" :href="href" target="_blank">
          <app-avatar :display-name="displayName" :email-address="actor.emailAddress" :icon="icon" :url="actor.picture" :variant="variant" />
        </a>
        <app-avatar v-else :display-name="displayName" :email-address="actor.emailAddress" :icon="icon" :url="actor.picture" :variant="variant" />
      </div>
    </div>
    <div>
      {{ d(date, "medium") }}
      <br />
      {{ t("by") }}
      <a v-if="href" :href="href" target="_blank">{{ displayName }} <font-awesome-icon icon="fas fa-arrow-up-right-from-square" /></a>
      <template v-else>{{ displayName }}</template>
    </div>
  </div>
</template>
