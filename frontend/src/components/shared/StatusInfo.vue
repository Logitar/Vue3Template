<script setup lang="ts">
import type { Actor } from "@/types/Actor";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

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
        return `/api-keys/${id}`;
      case "User":
        return `/users/${id}`;
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
    {{ t(format, { date: d(new Date(date), "medium") }) }}
    <template v-if="actor.isDeleted">
      <app-avatar :displayName="actor.displayName" :emailAddress="actor.emailAddress" :icon="icon" :size="24" :url="actor.picture" :variant="variant" />
      {{ actor.displayName }}
    </template>
    <a v-else :href="href" target="_blank">
      <app-avatar :displayName="actor.displayName" :emailAddress="actor.emailAddress" :icon="icon" :size="24" :url="actor.picture" :variant="variant" />
      {{ actor.displayName }} <font-awesome-icon icon="fas fa-arrow-up-right-from-square" />
    </a>
  </span>
</template>
