<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import EmailAddressInput from "@/components/Users/EmailAddressInput.vue";
import LocaleSelect from "@/components/Users/LocaleSelect.vue";
import PersonNameInput from "@/components/Users/PersonNameInput.vue";
import type { Profile } from "@/types/Profile";
import { getProfile, saveProfile } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";

const { d, t } = useI18n();

const emailAddress = ref<string>("");
const firstName = ref<string>("");
const lastName = ref<string>("");
const locale = ref<string>("");
const middleName = ref<string>("");
const nickname = ref<string>("");
const user = ref<Profile>();

function setModel(model: Profile): void {
  user.value = model;
  emailAddress.value = model.email.address;
  firstName.value = model.firstName;
  lastName.value = model.lastName;
  locale.value = model.locale;
  middleName.value = model.middleName ?? "";
  nickname.value = model.nickname ?? "";
}

onMounted(async () => {
  try {
    const { data } = await getProfile();
    setModel(data);
  } catch (e: any) {
    handleError(e);
  }
});

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    const { data } = await saveProfile({
      email: {
        address: emailAddress.value,
        verify: false,
      },
      firstName: firstName.value,
      middleName: middleName.value,
      lastName: lastName.value,
      nickname: nickname.value,
      locale: locale.value,
    });
    setModel(data);
  } catch (e) {
    handleError(e);
  }
});
</script>

<template>
  <div class="container">
    <h1>{{ t("users.profile.title") }}</h1>
    <h2>{{ t("users.personal.title") }}</h2>
    <table v-if="user" class="table table-striped">
      <tbody>
        <tr>
          <th scope="row">{{ t("users.name.full") }}</th>
          <td>{{ user.fullName }}</td>
        </tr>
        <tr>
          <th scope="row">{{ t("users.email.address.label") }}</th>
          <td>
            {{ user.email.address }}
            <app-badge v-if="user.email.isVerified">{{ t("users.email.verified") }}</app-badge>
          </td>
        </tr>
        <tr v-if="user.phone">
          <th scope="row">{{ t("users.phone.e164") }}</th>
          <td>
            {{ user.phone.e164Formatted }}
            <app-badge v-if="user.phone.isVerified">{{ t("users.phone.verified") }}</app-badge>
          </td>
        </tr>
        <tr>
          <th scope="row">{{ t("users.createdOn") }}</th>
          <td>{{ d(user.createdOn, "medium") }}</td>
        </tr>
        <tr>
          <th scope="row">{{ t("users.updatedOn") }}</th>
          <td>{{ d(user.updatedOn, "medium") }}</td>
        </tr>
        <tr v-if="user.signedInOn">
          <th scope="row">{{ t("users.signedInOn") }}</th>
          <td>{{ d(user.signedInOn, "medium") }}</td>
        </tr>
      </tbody>
    </table>
    <form @submit.prevent="onSubmit">
      <div class="row">
        <EmailAddressInput :disabled="user?.email.isVerified" required validate :verified="user?.email.isVerified" v-model="emailAddress" />
      </div>
      <!-- TODO(fpion): Phone -->
      <div class="row">
        <PersonNameInput class="col-lg-6" required type="first" validate v-model="firstName" />
        <PersonNameInput class="col-lg-6" required type="last" validate v-model="lastName" />
      </div>
      <div class="row">
        <PersonNameInput class="col-lg-6" type="middle" validate v-model="middleName" />
        <PersonNameInput class="col-lg-6" type="nick" validate v-model="nickname" />
      </div>
      <div class="row">
        <LocaleSelect class="col-lg-6" required v-model="locale" />
      </div>
      <icon-submit :disabled="isSubmitting" icon="fas fa-floppy-disk" :loading="isSubmitting" text="users.profile.submit" />
    </form>
  </div>
</template>
