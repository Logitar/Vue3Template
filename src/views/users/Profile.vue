<script setup lang="ts">
import { inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import AuthenticationInformation from "@/components/users/AuthenticationInformation.vue";
import ContactInformation from "@/components/users/ContactInformation.vue";
import PersonalInformation from "@/components/users/PersonalInformation.vue";
import ProfileHeader from "@/components/users/ProfileHeader.vue";
import type { ProfileUpdatedEvent, UserProfile } from "@/types/users";
import type { ToastUtils } from "@/types/components";
import { getProfile } from "@/api/account";
import { handleErrorKey, toastsKey } from "@/inject/App";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();
const { query } = useRoute();
const handleError = inject(handleErrorKey) as (e: unknown) => void;
const toasts = inject(toastsKey) as ToastUtils;

const account = useAccountStore();

const confirmed = ref<boolean>(query.status === "confirmed");
const user = ref<UserProfile>();

function onProfileUpdated(event: ProfileUpdatedEvent): void {
  user.value = event.user;
  account.signIn(event.user);

  if (event.toast ?? true) {
    toasts.success("users.profile.updated");
  }
}

onMounted(async () => {
  try {
    user.value = await getProfile();
  } catch (e: unknown) {
    handleError(e);
  }
});
</script>

<template>
  <main class="container">
    <h1>{{ t("users.profile.title") }}</h1>
    <app-alert dismissible variant="success" v-model="confirmed">
      <strong>{{ t("users.profile.welcome", { brand: t("brand") }) }}</strong> {{ t("users.profile.confirmed") }}
    </app-alert>
    <template v-if="user">
      <ProfileHeader :user="user" />
      <app-tabs>
        <app-tab active title="users.tabs.personal">
          <PersonalInformation :user="user" @profile-updated="onProfileUpdated" />
        </app-tab>
        <app-tab title="users.tabs.contact">
          <ContactInformation :user="user" @profile-updated="onProfileUpdated" />
        </app-tab>
        <app-tab title="users.tabs.authentication">
          <AuthenticationInformation :user="user" @profile-updated="onProfileUpdated" />
        </app-tab>
      </app-tabs>
    </template>
  </main>
</template>
