<script setup lang="ts">
import { inject, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import UsernameInput from "@/components/users/UsernameInput.vue";
import { handleErrorKey } from "@/inject/App";
import { recoverPassword } from "@/api/account";

const { t } = useI18n();
const handleError = inject(handleErrorKey) as (e: unknown) => void;

const success = ref<boolean>(false);
const username = ref<string>("");

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    await recoverPassword({ username: username.value });
    success.value = true;
  } catch (e: unknown) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container">
    <h1>{{ t("users.password.recover.title") }}</h1>
    <app-alert v-if="success" show variant="success">
      <strong>{{ t("users.password.recover.success") }}</strong> {{ t("users.password.recover.close") }}
    </app-alert>
    <template v-else>
      <app-alert show variant="info">{{ t("users.password.recover.info") }}</app-alert>
      <form @submit.prevent="onSubmit">
        <UsernameInput placeholder="users.usernameOrEmailPlaceholder" required v-model="username" />
        <icon-submit :disabled="isSubmitting" icon="fas fa-paper-plane" :loading="isSubmitting" text="users.password.recover.submit" />
        <RouterLink :to="{ name: 'SignIn' }" class="mx-1">{{ t("users.password.recover.remember") }}</RouterLink>
      </form>
    </template>
  </div>
</template>
