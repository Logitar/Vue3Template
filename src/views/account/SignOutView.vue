<script setup lang="ts">
import { onMounted } from "vue";
import { useRouter } from "vue-router";

import { signOut } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const router = useRouter();

onMounted(async () => {
  try {
    await signOut();
    account.signOut();
    router.push({ name: "SignIn" });
  } catch (e) {
    console.error(e); // TODO(fpion): error handling
  }
});
</script>

<template>
  <main class="container"></main>
</template>
