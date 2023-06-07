<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import EmailAddressInput from "@/components/Users/EmailAddressInput.vue";
import GenderSelect from "@/components/Users/GenderSelect.vue";
import LocaleSelect from "@/components/Users/LocaleSelect.vue";
import PersonNameInput from "@/components/Users/PersonNameInput.vue";
import PictureInput from "@/components/Users/PictureInput.vue";
import ProfileInput from "@/components/Users/ProfileInput.vue";
import TimeZoneSelect from "@/components/Users/TimeZoneSelect.vue";
import WebsiteInput from "@/components/Users/WebsiteInput.vue";
import type { UserProfile } from "@/types/UserProfile";
import { getProfile, saveProfile } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";

const { d, t } = useI18n();

// const { query } = useRoute(); // TODO(fpion): implement
// const activated = ref(query.status === "activated"); // TODO(fpion): implement

const emailAddress = ref<string>("");
const firstName = ref<string>("");
const gender = ref<string>("");
const lastName = ref<string>("");
const locale = ref<string>("");
const middleName = ref<string>("");
const nickname = ref<string>("");
const picture = ref<string>("");
const profile = ref<string>("");
const timeZone = ref<string>("");
const user = ref<UserProfile>();
const website = ref<string>("");

function setModel(model: UserProfile): void {
  user.value = model;
  emailAddress.value = model.email.address;
  firstName.value = model.firstName;
  gender.value = model.gender ?? "";
  lastName.value = model.lastName;
  locale.value = model.locale;
  middleName.value = model.middleName ?? "";
  nickname.value = model.nickname ?? "";
  picture.value = model.picture ?? "";
  profile.value = model.profile ?? "";
  timeZone.value = model.timeZone ?? "";
  website.value = model.website ?? "";
}

onMounted(async () => {
  try {
    const { data } = await getProfile();
    setModel(data);
    // TODO(fpion): visual feedback?
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
      gender: gender.value,
      middleName: middleName.value,
      lastName: lastName.value,
      nickname: nickname.value,
      locale: locale.value,
      timeZone: timeZone.value,
      picture: picture.value,
      profile: profile.value,
      website: website.value,
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
      <!-- TODO(fpion): Address -->
      <EmailAddressInput :disabled="user?.email.isVerified" required validate :verified="user?.email.isVerified" v-model="emailAddress" />
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
        <div class="col-lg-6"><!-- TODO(fpion): Birthdate --></div>
        <GenderSelect class="col-lg-6" v-model="gender" />
      </div>
      <div class="row">
        <LocaleSelect class="col-lg-6" required v-model="locale" />
        <TimeZoneSelect class="col-lg-6" v-model="timeZone" />
      </div>
      <PictureInput v-model="picture" />
      <ProfileInput v-model="profile" />
      <WebsiteInput v-model="website" />
      <icon-submit :disabled="isSubmitting" icon="fas fa-floppy-disk" :loading="isSubmitting" text="actions.save" />
    </form>
  </div>
</template>
