<script setup lang="ts">
import { computed, ref, watchEffect } from "vue";
import { useForm } from "vee-validate";

import AppSaveButton from "@/components/shared/AppSaveButton.vue";
import BirthdateInput from "@/components/users/BirthdateInput.vue";
import GenderSelect from "@/components/users/GenderSelect.vue";
import LocaleSelect from "@/components/users/LocaleSelect.vue";
import PersonNameInput from "@/components/users/PersonNameInput.vue";
import PictureInput from "@/components/users/PictureInput.vue";
import ProfileInput from "@/components/users/ProfileInput.vue";
import TimeZoneSelect from "@/components/users/TimeZoneSelect.vue";
import WebsiteInput from "@/components/users/WebsiteInput.vue";
import type { UserProfile } from "@/types/account";
import { saveProfile } from "@/api/account";

const props = defineProps<{
  user: UserProfile;
}>();

const birthdate = ref<Date>();
const firstName = ref<string>("");
const gender = ref<string>("");
const lastName = ref<string>("");
const locale = ref<string>("");
const middleName = ref<string>("");
const nickname = ref<string>("");
const picture = ref<string>("");
const profile = ref<string>("");
const timeZone = ref<string>("");
const website = ref<string>("");

const hasChanges = computed<boolean>(() => {
  const user: UserProfile = props.user;
  return (
    firstName.value !== (user.firstName ?? "") ||
    lastName.value !== (user.lastName ?? "") ||
    middleName.value !== (user.middleName ?? "") ||
    nickname.value !== (user.nickname ?? "") ||
    birthdate.value?.getTime() !== (user.birthdate ? new Date(user.birthdate).getTime() : undefined) ||
    gender.value !== (user.gender ?? "") ||
    locale.value !== (user.locale?.code ?? "") ||
    timeZone.value !== (user.timeZone ?? "") ||
    picture.value !== (user.picture ?? "") ||
    profile.value !== (user.profile ?? "") ||
    website.value !== (user.website ?? "")
  );
});

const emit = defineEmits<{
  (e: "error", value: unknown): void;
  (e: "saved", value: UserProfile): void;
}>();

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    const user: UserProfile = await saveProfile({
      personalInformation: {
        firstName: firstName.value,
        middleName: middleName.value,
        lastName: lastName.value,
        nickname: nickname.value,
        birthdate: birthdate.value,
        gender: gender.value,
        locale: locale.value,
        timeZone: timeZone.value,
        picture: picture.value,
        profile: profile.value,
        website: website.value,
      },
    });
    emit("saved", user);
  } catch (e: unknown) {
    emit("error", e);
  }
});

watchEffect(() => {
  const user: UserProfile = props.user;
  birthdate.value = user.birthdate ? new Date(user.birthdate) : undefined;
  firstName.value = user.firstName ?? "";
  gender.value = user.gender ?? "";
  lastName.value = user.lastName ?? "";
  locale.value = user.locale?.code ?? "";
  middleName.value = user.middleName ?? "";
  nickname.value = user.nickname ?? "";
  picture.value = user.picture ?? "";
  profile.value = user.profile ?? "";
  timeZone.value = user.timeZone ?? "";
  website.value = user.website ?? "";
});
</script>

<template>
  <form @submit.prevent="onSubmit">
    <div class="mb-3">
      <AppSaveButton :disabled="isSubmitting || !hasChanges" exists :loading="isSubmitting" />
    </div>
    <div class="row">
      <PersonNameInput class="col-lg-6" type="first" v-model="firstName" />
      <PersonNameInput class="col-lg-6" type="last" v-model="lastName" />
    </div>
    <div class="row">
      <PersonNameInput class="col-lg-6" type="middle" v-model="middleName" />
      <PersonNameInput class="col-lg-6" type="nick" v-model="nickname" />
    </div>
    <div class="row">
      <BirthdateInput class="col-lg-6" v-model="birthdate" />
      <GenderSelect class="col-lg-6" v-model="gender" />
    </div>
    <div class="row">
      <LocaleSelect class="col-lg-6" v-model="locale" />
      <TimeZoneSelect class="col-lg-6" v-model="timeZone" />
    </div>
    <PictureInput v-model="picture" />
    <ProfileInput v-model="profile" />
    <WebsiteInput v-model="website" />
  </form>
</template>
