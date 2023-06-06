<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import type { ApiResult } from "@/types/ApiResult";
import { confirm } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";

const { t } = useI18n();

const error = ref<boolean>(false);

const { query } = useRoute();
const router = useRouter();
onMounted(async () => {
  const token = query.token?.toString();
  if (token) {
    try {
      await confirm({ token });
      router.push({ name: "Profile", query: { status: "confirmed" } });
    } catch (e: any) {
      const result = e as ApiResult;
      if (result.status === 400 && result.data?.code === "InvalidCredentials") {
        router.push({ name: "SignIn" });
      } else {
        handleError(e);
        error.value = true;
      }
    }
  } else {
    router.push({ name: "SignIn" });
  }
});
</script>

<template>
  <div class="container">
    <app-alert v-if="error" show>
      <strong>{{ t("users.confirm.failed") }}</strong> {{ t("users.confirm.signIn") }}
      <br />
      <icon-button icon="fas fa-arrow-right-to-bracket" text="users.signIn.title" :to="{ name: 'SignIn' }" />
    </app-alert>
  </div>
</template>
