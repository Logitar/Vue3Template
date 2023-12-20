<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

import type { Actor } from "@/types/aggregate";
import type { ResetPasswordPayload } from "@/types/account";
import { resetPassword } from "@/api/account";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const route = useRoute();
const router = useRouter();

const loading = ref<boolean>(false);
const passwordConfirmation = ref<string>();
const payload = ref<ResetPasswordPayload>({ token: "", password: "" });

async function submit(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    try {
      const actor: Actor | undefined = await resetPassword(payload.value);
      if (!actor) {
        router.push({ name: "SignIn" });
        return;
      }
      account.signIn(actor);
      router.push({ name: "Profile" });
    } catch (e) {
      console.error(e); // TODO(fpion): error handling
    } finally {
      loading.value = false;
    }
  }
}

onMounted(() => (payload.value.token = route.params.token as string));
</script>

<template>
  <main class="container">
    <h1>Reset Password</h1>
    <form @submit.prevent="submit">
      <div class="row">
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="password">Password <span class="text-danger">*</span></label>
            <input class="form-control" id="password" placeholder="Password" required type="password" v-model="payload.password" />
          </div>
        </div>
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="password-confirmation"> Password Confirmation <span class="text-danger">*</span> </label>
            <input
              class="form-control"
              id="password-confirmation"
              placeholder="Password Confirmation"
              required
              type="password"
              v-model="passwordConfirmation"
            />
          </div>
        </div>
      </div>
      <button class="btn btn-primary" :disabled="loading" type="submit">
        <span v-if="loading">
          <span class="spinner-border spinner-border-sm" aria-hidden="true"></span>
          <span class="visually-hidden">Loading...</span>
        </span>
        <FontAwesomeIcon v-else :icon="['fas', 'key']" />
        Reset Password
      </button>
    </form>
  </main>
</template>
