<script setup lang="ts">
import { inject, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import EmailAddressInput from "@/components/users/EmailAddressInput.vue";
import PasswordInput from "@/components/users/PasswordInput.vue";
import PersonNameInput from "@/components/users/PersonNameInput.vue";
import UsernameInput from "@/components/users/UsernameInput.vue";
import { handleErrorKey } from "@/inject/App";
import { register } from "@/api/account";

const { t, locale } = useI18n();
const handleError = inject(handleErrorKey) as (e: unknown) => void;

const confirm = ref<string>("");
const emailAddress = ref<string>("");
const firstName = ref<string>("");
const lastName = ref<string>("");
const password = ref<string>("");
const success = ref<boolean>(false);
const username = ref<string>("");

function onEmailAddressInput(value: string): void {
  emailAddress.value = value;
  username.value = value;
}

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    await register({
      username: username.value,
      password: password.value,
      emailAddress: emailAddress.value,
      firstName: firstName.value,
      lastName: lastName.value,
      locale: locale.value,
    });
    success.value = true;
  } catch (e: unknown) {
    handleError(e);
  }
});
</script>

<template>
  <main class="container">
    <h1>{{ t("users.register.title") }}</h1>
    <app-alert v-if="success" show variant="success">
      <strong>{{ t("users.register.success") }}</strong>
      <br />
      {{ t("users.register.close") }}
    </app-alert>
    <form v-else @submit.prevent="onSubmit">
      <div class="row">
        <EmailAddressInput class="col-lg-6" :model-value="emailAddress" required validate @update:model-value="onEmailAddressInput" />
        <UsernameInput class="col-lg-6" required validate v-model="username" />
      </div>
      <div class="row">
        <PersonNameInput class="col-lg-6" required type="first" validate v-model="firstName" />
        <PersonNameInput class="col-lg-6" required type="last" validate v-model="lastName" />
      </div>
      <div class="row">
        <PasswordInput class="col-lg-6" required validate v-model="password" />
        <PasswordInput
          class="col-lg-6"
          :confirm="{ value: password, label: 'users.password.label' }"
          id="confirm"
          label="users.password.confirm.label"
          placeholder="users.password.confirm.placeholder"
          required
          validate
          v-model="confirm"
        />
      </div>
      <icon-submit :disabled="isSubmitting" icon="fas fa-user" :loading="isSubmitting" text="users.register.submit" />
    </form>
  </main>
</template>
