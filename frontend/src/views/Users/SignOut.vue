<script setup lang="ts">
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { handleError } from "@/helpers/errorUtils";
import { signOut } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();

const router = useRouter();
onMounted(async () => {
  try {
    await signOut();
    account.signOut();
    router.push({ name: "SignIn" });
  } catch (e: any) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container"></div>
</template>
