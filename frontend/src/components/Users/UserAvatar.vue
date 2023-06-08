<script setup lang="ts">
import { computed } from "vue";
import md5 from "md5";
import type { UserProfile } from "@/types/UserProfile";

const props = withDefaults(
  defineProps<{
    size?: number;
    user: UserProfile;
  }>(),
  {
    size: 32,
  }
);

const alt = computed<string>(() => `${props.user.fullName}'s Avatar`);
const src = computed<string>(() => props.user.picture ?? `https://www.gravatar.com/avatar/${md5(props.user.email.address)}`);
</script>

<template>
  <img class="rounded-circle" :src="src" :alt="alt" :height="size" />
</template>
