<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { TarCheckbox } from "logitar-vue3-ui";
import { ref } from "vue";
import { useRouter } from "vue-router";

import type { RegisterPayload } from "@/types/account";
import { register } from "@/api/account";

const router = useRouter();

const loading = ref<boolean>(false);
const passwordConfirmation = ref<string>();
const payload = ref<RegisterPayload>({ username: "" });
const success = ref<boolean>(false);

async function submit(): Promise<void> {
  if (!loading.value) {
    loading.value = true;
    try {
      await register(payload.value);
      payload.value.password = undefined;
      passwordConfirmation.value = undefined;
      success.value = true;
    } catch (e) {
      console.error(e); // TODO(fpion): error handling
    } finally {
      loading.value = false;
    }
  }
}

function onConfirm(): void {
  if (payload.value.emailAddress) {
    router.push({ name: "Confirm", params: { token: payload.value.emailAddress } });
  }
}

function onEmailAddressUpdate(e: Event): void {
  const value = (e.target as HTMLInputElement).value.trim();
  payload.value.emailAddress = value;
  payload.value.username = value;
}
</script>

<template>
  <main class="container">
    <h1>Register</h1>
    <TarCheckbox class="mb-3" id="success" label="Success" switch v-model="success" />
    <div v-if="success">
      <div class="alert alert-success">Success!</div>
      <button v-if="payload.emailAddress" type="button" class="btn btn-warning" @click="onConfirm">
        <FontAwesomeIcon :icon="['fas', 'user']" />
        Confirm your account
      </button>
    </div>
    <form v-else @submit.prevent="submit">
      <div class="row">
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="email-address">Email Address</label>
            <input
              class="form-control"
              id="email-address"
              maxlength="255"
              placeholder="Email Address"
              type="email"
              :value="payload.emailAddress"
              @input="onEmailAddressUpdate"
            />
          </div>
        </div>
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="username">Username <span class="text-danger">*</span></label>
            <input class="form-control" id="username" maxlength="255" placeholder="Username" required type="text" v-model.trim="payload.username" />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="first-name">First Name</label>
            <input class="form-control" id="first-name" maxlength="255" placeholder="First Name" type="text" v-model.trim="payload.firstName" />
          </div>
        </div>
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="last-name">Last Name</label>
            <input class="form-control" id="last-name" maxlength="255" placeholder="Last Name" type="text" v-model.trim="payload.lastName" />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="password">Password</label>
            <input class="form-control" id="password" placeholder="Password" type="password" v-model="payload.password" />
          </div>
        </div>
        <div class="col">
          <div class="mb-3">
            <label class="form-label" for="password-confirmation">
              Password Confirmation <span v-if="Boolean(payload.password)" class="text-danger">*</span>
            </label>
            <input
              class="form-control"
              id="password-confirmation"
              placeholder="Password Confirmation"
              :required="Boolean(payload.password)"
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
        <FontAwesomeIcon v-else :icon="['fas', 'user']" />
        Register
      </button>
    </form>
  </main>
</template>
