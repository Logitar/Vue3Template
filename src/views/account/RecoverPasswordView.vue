<script setup lang="ts">
import { TarButton, TarCheckbox } from "logitar-vue3-ui";
import { ref } from "vue";
import { useRouter } from "vue-router";

import type { RecoverPasswordPayload } from "@/types/account";
import { recoverPassword } from "@/api/account";

const router = useRouter();

const loading = ref<boolean>(false);
const payload = ref<RecoverPasswordPayload>({ username: "" });
const success = ref<boolean>(false);

async function submit(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    try {
      await recoverPassword(payload.value);
      success.value = true;
    } catch (e) {
      console.error(e); // TODO(fpion): error handling
    }
  }
}

function onResetPassword(): void {
  if (payload.value.username) {
    router.push({ name: "ResetPassword", params: { token: payload.value.username } });
  }
}
</script>

<template>
  <main class="container">
    <h1>Recover Password</h1>
    <TarCheckbox class="mb-3" id="success" label="Success" switch v-model="success" />
    <div v-if="success">
      <div class="alert alert-success">Success!</div>
      <TarButton v-if="payload.username" :icon="['fas', 'key']" text="Reset your password" variant="warning" @click="onResetPassword" />
    </div>
    <form v-else @submit.prevent="submit">
      <div class="mb-3">
        <label class="form-label" for="username">Username or Email Address <span class="text-danger">*</span></label>
        <input class="form-control" id="username" placeholder="Username or Email Address" required type="text" v-model.trim="payload.username" />
      </div>
      <TarButton class="me-2" :disabled="loading" :icon="['fas', 'key']" :loading="loading" text="Recover Password" type="submit" />
      <RouterLink :to="{ name: 'SignIn' }">I remember my password</RouterLink>
    </form>
  </main>
</template>
