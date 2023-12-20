<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";

import type { Actor } from "@/types/aggregate";
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
    } catch (e) {
      if (e === "INVALID_CREDENTIALS") {
        invalidCredentials.value = true;
      } else {
        console.error(e); // TODO(fpion): error handling
      }
    } finally {
      loading.value = false;
    }
  }
}

import { useUserStore } from "@/stores/user";
const users = useUserStore();
users.create({
  username: "fpion",
});
</script>

<template>
  <main class="container">
    <h1>Sign In</h1>
    <div v-if="invalidCredentials" class="alert alert-warning">Invalid Credentials!</div>
    <form @submit.prevent="submit">
      <div class="mb-3">
        <label class="form-label" for="username">Username or Email Address <span class="text-danger">*</span></label>
        <input class="form-control" id="username" placeholder="Username or Email Address" required type="text" v-model.trim="payload.username" />
      </div>
      <div class="mb-3">
        <label class="form-label" for="password">Password</label>
        <input class="form-control" id="password" placeholder="Password" type="password" v-model="payload.password" />
      </div>
      <div class="form-check mb-3">
        <input class="form-check-input" id="remember-me" type="checkbox" v-model="payload.remember" />
        <label class="form-check-label" for="remember-me">Remember me</label>
      </div>
      <button class="btn btn-primary" :disabled="loading" type="submit">
        <span v-if="loading">
          <span class="spinner-border spinner-border-sm" aria-hidden="true"></span>
          <span class="visually-hidden">Loading...</span>
        </span>
        <FontAwesomeIcon v-else :icon="['fas', 'right-to-bracket']" />
        Sign In
      </button>
    </form>
  </main>
</template>
