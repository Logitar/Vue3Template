<script setup lang="ts">
import { inject, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import PasswordInput from "@/components/users/PasswordInput.vue";
import UsernameInput from "@/components/users/UsernameInput.vue";
import type { ApiError, ErrorDetail } from "@/types/api";
import { handleErrorKey } from "@/inject/App";
import { signIn } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();
const handleError = inject(handleErrorKey) as (e: unknown) => void;

const account = useAccountStore();

const invalidCredentials = ref<boolean>(false);
const password = ref<string>("");
const passwordRef = ref<InstanceType<typeof PasswordInput> | null>(null);
const remember = ref<boolean>(false);
const username = ref<string>("");

const { handleSubmit, isSubmitting } = useForm();
const route = useRoute();
const router = useRouter();
const onSubmit = handleSubmit(async () => {
  try {
    invalidCredentials.value = false;
    const user = await signIn({ username: username.value, password: password.value, remember: remember.value });
    account.signIn(user);
    const redirect: string | undefined = route.query.redirect?.toString();
    router.push(redirect ?? { name: "Profile" });
  } catch (e: unknown) {
    const { data, status } = e as ApiError;
    if (status === 400 && (data as ErrorDetail)?.errorCode === "InvalidCredentials") {
      invalidCredentials.value = true;
      password.value = "";
      passwordRef.value?.focus();
    } else {
      handleError(e);
    }
  }
});
</script>

<template>
  <main class="container">
    <h1>{{ t("users.signIn.title") }}</h1>
    <app-alert dismissible variant="warning" v-model="invalidCredentials">
      <strong>{{ t("users.signIn.failed") }}</strong> {{ t("users.signIn.invalidCredentials") }}
    </app-alert>
    <form @submit.prevent="onSubmit">
      <UsernameInput placeholder="users.usernameOrEmailPlaceholder" required v-model="username" />
      <PasswordInput ref="passwordRef" required v-model="password" />
      <form-checkbox class="mb-3" id="remember" label="users.signIn.remember" v-model="remember" />
      <icon-submit class="me-2" :disabled="isSubmitting" icon="fas fa-arrow-right-to-bracket" :loading="isSubmitting" text="users.signIn.submit" />
      <RouterLink :to="{ name: 'RecoverPassword' }" class="mx-1">{{ t("users.signIn.recover") }}</RouterLink>
    </form>
  </main>
</template>
