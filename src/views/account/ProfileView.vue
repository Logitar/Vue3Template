<script setup lang="ts">
import { TarTab, TarTabs } from "logitar-vue3-ui";
import { inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";

import AuthenticationInformation from "@/components/users/AuthenticationInformation.vue";
import ContactInformation from "@/components/users/ContactInformation.vue";
import PersonalInformation from "@/components/users/PersonalInformation.vue";
import ProfileHeader from "@/components/users/ProfileHeader.vue";
import type { UserProfile } from "@/types/account";
import { getProfile } from "@/api/account";
import { handleErrorKey } from "@/inject/App";
import { useAccountStore } from "@/stores/account";
import { useToastStore } from "@/stores/toast";

const account = useAccountStore();
const handleError = inject(handleErrorKey) as (e: unknown) => void;
const toasts = useToastStore();
const { t } = useI18n();

const user = ref<UserProfile>();

function onSaved(profile: UserProfile, message?: string) {
  user.value = profile;
  account.signIn({
    displayName: profile.fullName ?? profile.username,
    emailAddress: profile.email?.address,
    pictureUrl: profile.picture,
  });
  toasts.success(message ?? "users.profile.saved");
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
    <template v-if="user">
      <h1>{{ t("users.profile.title") }}</h1>
      <ProfileHeader :user="user" />
      <TarTabs>
        <TarTab active id="authentication" :title="t('users.tabs.authentication')">
          <AuthenticationInformation :user="user" @error="handleError" @saved="onSaved($event, 'users.password.changed')" />
        </TarTab>
        <TarTab id="contact" :title="t('users.tabs.contact')">
          <ContactInformation :user="user" @error="handleError" @saved="onSaved" />
        </TarTab>
        <TarTab id="personal" :title="t('users.tabs.personal')">
          <PersonalInformation :user="user" @error="handleError" @saved="onSaved" />
        </TarTab>
      </TarTabs>
    </template>
  </main>
</template>
