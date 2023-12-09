<script setup lang="ts">
import { inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import type { ApiError, ErrorDetail } from "@/types/api";
import { confirm } from "@/api/account";
import { handleErrorKey } from "@/inject/App";

const { t } = useI18n();
const handleError = inject(handleErrorKey) as (e: unknown) => void;

const error = ref<boolean>(false);

const { query } = useRoute();
const router = useRouter();
onMounted(async () => {
  const token = query.token?.toString();
  if (token) {
    try {
      await confirm({ token });
      router.push({ name: "Profile", query: { status: "confirmed" } });
    } catch (e: unknown) {
      const { data, status } = e as ApiError;
      if (status === 400 && (data as ErrorDetail)?.errorCode === "InvalidCredentials") {
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
  <main class="container">
    <app-alert v-if="error" show>
      <strong>{{ t("users.confirm.failed") }}</strong> {{ t("users.confirm.signIn") }}
      <br />
      <icon-button icon="fas fa-arrow-right-to-bracket" text="users.signIn.title" :to="{ name: 'SignIn' }" />
    </app-alert>
  </main>
</template>
