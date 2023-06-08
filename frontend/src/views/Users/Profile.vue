<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import AuthenticationInformation from "@/components/Users/AuthenticationInformation.vue";
import ContactInformation from "@/components/Users/ContactInformation.vue";
import PersonalInformation from "@/components/Users/PersonalInformation.vue";
import ProfileHeader from "@/components/Users/ProfileHeader.vue";
import type { UserProfile } from "@/types/UserProfile";
import { getProfile } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();
const { query } = useRoute();

const account = useAccountStore();

const confirmed = ref<boolean>(query.status === "confirmed");
const user = ref<UserProfile>();

function onProfileUpdated(value: UserProfile): void {
  user.value = value;
  account.signIn(value);
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
        <template #headers>
          <tab-header active id="personalHeader" target="personalContents">{{ t("users.tabs.personal") }}</tab-header>
          <tab-header id="contactHeader" target="contactContents">{{ t("users.tabs.contact") }}</tab-header>
          <tab-header id="authenticationHeader" target="authenticationContents">{{ t("users.tabs.authentication") }}</tab-header>
        </template>
        <template #contents>
          <tab-contents active header="personalHeader" id="personalContents">
            <PersonalInformation :user="user" @profileUpdated="onProfileUpdated" />
          </tab-contents>
          <tab-contents header="contactHeader" id="contactContents">
            <ContactInformation :user="user" @profileUpdated="onProfileUpdated" />
          </tab-contents>
          <tab-contents header="authenticationHeader" id="authenticationContents">
            <AuthenticationInformation :user="user" @profileUpdated="onProfileUpdated" />
          </tab-contents>
        </template>
      </app-tabs>
    </template>
  </div>
</template>
