<script setup lang="ts">
import { ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import PasswordInput from "@/components/Users/PasswordInput.vue";
import type { ApiResult } from "@/types/ApiResult";
import { handleError } from "@/helpers/errorUtils";
import { onMounted } from "vue";
import { resetPassword, validatePasswordReset } from "@/api/account";

const { t } = useI18n();

const confirm = ref<string>("");
const password = ref<string>("");
const success = ref<boolean>(false);

const { query } = useRoute();
const token = query.token?.toString() ?? "";
const router = useRouter();
onMounted(async () => {
  if (token) {
    try {
      await validatePasswordReset(token);
    } catch (e: any) {
      const { data, status } = e as ApiResult;
      if (status === 400 && data?.code === "InvalidCredentials") {
        router.push({ name: "SignIn" });
      } else {
        handleError(e);
      }
    }
  } else {
    router.push({ name: "SignIn" });
  }
});

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    await resetPassword({ token, password: password.value });
    success.value = true;
  } catch (e) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container">
    <h1>{{ t("users.password.reset.title") }}</h1>
    <app-alert v-if="success" show variant="success">
      <strong>{{ t("users.password.reset.success") }}</strong> {{ t("users.password.reset.signIn") }}
      <br />
      <icon-button icon="fas fa-arrow-right-to-bracket" text="users.signIn.title" :to="{ name: 'SignIn' }" />
    </app-alert>
    <template v-else>
      <app-alert show variant="info">{{ t("users.password.reset.info") }}</app-alert>
      <form @submit.prevent="onSubmit">
        <PasswordInput required validate v-model="password" />
        <PasswordInput
          :confirm="{ value: password, label: 'users.password.label' }"
          id="confirm"
          label="users.password.confirm.label"
          placeholder="users.password.confirm.placeholder"
          required
          validate
          v-model="confirm"
        />
        <icon-submit :disabled="isSubmitting" icon="fas fa-key" :loading="isSubmitting" text="users.password.reset.submit" />
      </form>
    </template>
  </div>
</template>
