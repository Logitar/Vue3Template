<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import AuthenticationInformation from "@/components/Users/AuthenticationInformation.vue";
import ContactInformation from "@/components/Users/ContactInformation.vue";
import PersonalInformation from "@/components/Users/PersonalInformation.vue";
import ProfileHeader from "@/components/Users/ProfileHeader.vue";
import type { ProfileUpdatedEvent } from "@/types/ProfileUpdatedEvent";
import type { UserProfile } from "@/types/UserProfile";
import { getProfile } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";
import { toasts } from "@/helpers/toastUtils";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();
const { query } = useRoute();

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
    const { data } = await getProfile();
    user.value = data;
  } catch (e: any) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container">
    <h1>{{ t("users.profile.title") }}</h1>
    <app-alert dismissible variant="success" v-model="confirmed">
      <strong>{{ t("users.profile.welcome", { brand: t("brand") }) }}</strong> {{ t("users.profile.confirmed") }}
    </app-alert>
    <template v-if="user">
      <ProfileHeader :user="user" />
      <app-tabs>
        <app-tab active id="personal" title="users.tabs.personal">
          <PersonalInformation :user="user" @profileUpdated="onProfileUpdated" />
        </app-tab>
        <app-tab id="contact" title="users.tabs.contact">
          <ContactInformation :user="user" @profileUpdated="onProfileUpdated" />
        </app-tab>
        <app-tab id="authentication" title="users.tabs.authentication">
          <AuthenticationInformation :user="user" @profileUpdated="onProfileUpdated" />
        </app-tab>
      </app-tabs>
    </template>
  </div>
</template>
