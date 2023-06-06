<script setup lang="ts">
import { onMounted } from "vue";
import { useRouter } from "vue-router";
import { handleError } from "@/helpers/errorUtils";
import { signOut } from "@/api/account";

const router = useRouter();
onMounted(async () => {
  try {
    await signOut();
    router.push({ name: "SignIn" });
  } catch (e: any) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container"></div>
</template>
