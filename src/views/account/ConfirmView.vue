<script setup lang="ts">
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

import type { Actor } from "@/types/aggregate";
import { confirm } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const route = useRoute();
const router = useRouter();

onMounted(async () => {
  try {
    const token = route.params.token as string;
    const actor: Actor | undefined = await confirm(token);
    if (!actor) {
      router.push({ name: "SignIn" });
      return;
    }
    account.signIn(actor);
    router.push({ name: "Profile" });
  } catch (e) {
    console.error(e); // TODO(fpion): error handling
  }
});
</script>

<template>
  <main class="container"></main>
</template>
