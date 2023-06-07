<script setup lang="ts">
import { ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import PasswordInput from "@/components/Users/PasswordInput.vue";
import UsernameInput from "@/components/Users/UsernameInput.vue";
import { handleError } from "@/helpers/errorUtils";
import { signIn } from "@/api/account";
import type { ApiResult } from "@/types/ApiResult";
const { t } = useI18n();

const invalidCredentials = ref<boolean>(false);
const password = ref<string>("");
const passwordRef = ref<HTMLInputElement>();
const remember = ref<boolean>(false);
const username = ref<string>("");

const { handleSubmit, isSubmitting } = useForm();
const route = useRoute();
const router = useRouter();
const onSubmit = handleSubmit(async () => {
  try {
    invalidCredentials.value = false;
    await signIn({ username: username.value, password: password.value, remember: remember.value });
    const redirect: string | undefined = route.query.redirect?.toString();
    router.push(redirect ?? { name: "Profile" });
  } catch (e: any) {
    const { data, status } = e as ApiResult;
    if (status === 400 && data?.code === "InvalidCredentials") {
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
  <div class="container">
    <h1>{{ t("users.signIn.title") }}</h1>
    <app-alert dismissible variant="warning" v-model="invalidCredentials">
      <strong>{{ t("users.signIn.failed") }}</strong> {{ t("users.signIn.invalidCredentials") }}
    </app-alert>
    <form @submit.prevent="onSubmit">
      <UsernameInput placeholder="users.usernameOrEmailPlaceholder" required v-model="username" />
      <PasswordInput ref="passwordRef" required v-model="password" />
      <form-checkbox id="remember" label="users.signIn.remember" v-model="remember" />
      <icon-submit :disabled="isSubmitting" icon="fas fa-arrow-right-to-bracket" :loading="isSubmitting" text="users.signIn.submit" />
      <RouterLink :to="{ name: 'RecoverPassword' }" class="mx-1">{{ t("users.signIn.recover") }}</RouterLink>
    </form>
  </div>
</template>
