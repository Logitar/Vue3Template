<script setup lang="ts">
import { TarAlert, TarButton, TarCheckbox } from "logitar-vue3-ui";
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";

import type { Actor } from "@/types/aggregate";
import type { ErrorDetail } from "@/types/api";
import type { SignInPayload } from "@/types/account";
import { signIn } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const route = useRoute();
const router = useRouter();

const invalidCredentials = ref<boolean>(false);
const loading = ref<boolean>(false);
const payload = ref<SignInPayload>({ username: "", remember: false });

async function submit(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    invalidCredentials.value = false;
    try {
      const actor: Actor = await signIn(payload.value);
      account.signIn(actor);
      const redirect = route.query.redirect as string | undefined;
      router.push(redirect ?? { name: "Profile" });
    } catch (e: unknown) {
      const { code } = e as ErrorDetail;
      if (code === "InvalidCredentials") {
        invalidCredentials.value = true;
      } else {
        console.error(e); // TODO(fpion): error handling
      }
    } finally {
      loading.value = false;
    }
  }
}
</script>

<template>
  <main class="container">
    <h1>Sign In</h1>
    <TarAlert v-if="invalidCredentials" show variant="warning">Invalid Credentials!</TarAlert>
    <form @submit.prevent="submit">
      <div class="mb-3">
        <label class="form-label" for="username">Username or Email Address <span class="text-danger">*</span></label>
        <input class="form-control" id="username" placeholder="Username or Email Address" required type="text" v-model.trim="payload.username" />
      </div>
      <div class="mb-3">
        <label class="form-label" for="password">Password</label>
        <input class="form-control" id="password" placeholder="Password" type="password" v-model="payload.password" />
      </div>
      <TarCheckbox class="mb-3" id="remember-be" label="Remember Me" v-model="payload.remember" />
      <TarButton class="me-2" :disabled="loading" :icon="['fas', 'right-to-bracket']" :loading="loading" text="Sign In" type="submit" />
      <RouterLink :to="{ name: 'RecoverPassword' }">I forgot my password</RouterLink>
    </form>
  </main>
</template>
