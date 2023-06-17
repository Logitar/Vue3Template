<script setup lang="ts">
import { computed, inject, ref, watchEffect } from "vue";
import { useForm } from "vee-validate";
import BirthdateInput from "@/components/_users/BirthdateInput.vue";
import GenderSelect from "@/components/_users/GenderSelect.vue";
import PersonNameInput from "@/components/_users/PersonNameInput.vue";
import PictureInput from "@/components/_users/PictureInput.vue";
import ProfileInput from "@/components/_users/ProfileInput.vue";
import TimeZoneSelect from "@/components/_users/TimeZoneSelect.vue";
import WebsiteInput from "@/components/_users/WebsiteInput.vue";
import type { ProfileUpdatedEvent } from "@/types/ProfileUpdatedEvent";
import type { UserProfile } from "@/types/UserProfile";
import { handleErrorKey } from "@/inject/App";
import { savePersonalInformation } from "@/api/account";

const handleError = inject(handleErrorKey) as (e: unknown) => void;

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
  const user: UserProfile | undefined = props.user;
  if (!user) {
    return false;
  }
  return (
    firstName.value !== user.firstName ||
    lastName.value !== user.lastName ||
    middleName.value !== (user.middleName ?? "") ||
    nickname.value !== (user.nickname ?? "") ||
    birthdate.value?.getTime() !== (user.birthdate ? new Date(user.birthdate) : undefined)?.getTime() ||
    gender.value !== (user.gender ?? "") ||
    locale.value !== (user.locale ?? "") ||
    timeZone.value !== (user.timeZone ?? "") ||
    picture.value !== (user.picture ?? "") ||
    profile.value !== (user.profile ?? "") ||
    website.value !== (user.website ?? "")
  );
});

watchEffect(() => {
  const user = props.user;
  birthdate.value = user.birthdate ? new Date(user.birthdate) : undefined;
  firstName.value = user.firstName;
  gender.value = user.gender ?? "";
  lastName.value = user.lastName;
  locale.value = user.locale;
  middleName.value = user.middleName ?? "";
  nickname.value = user.nickname ?? "";
  picture.value = user.picture ?? "";
  profile.value = user.profile ?? "";
  timeZone.value = user.timeZone ?? "";
  website.value = user.website ?? "";
});

const emit = defineEmits<{
  (e: "profile-updated", event: ProfileUpdatedEvent): void;
}>();
const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    const { data } = await savePersonalInformation({
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
    });
    emit("profile-updated", { user: data });
  } catch (e: unknown) {
    handleError(e);
  }
});
</script>

<template>
  <div>
    <form @submit.prevent="onSubmit">
      <div class="mb-3">
        <icon-submit :disabled="!hasChanges || isSubmitting" icon="fas fa-floppy-disk" :loading="isSubmitting" text="actions.save" />
      </div>
      <div class="row">
        <PersonNameInput class="col-lg-6" required type="first" validate v-model="firstName" />
        <PersonNameInput class="col-lg-6" required type="last" validate v-model="lastName" />
      </div>
      <div class="row">
        <PersonNameInput class="col-lg-6" type="middle" validate v-model="middleName" />
        <PersonNameInput class="col-lg-6" type="nick" validate v-model="nickname" />
      </div>
      <div class="row">
        <BirthdateInput class="col-lg-6" v-model="birthdate" />
        <GenderSelect class="col-lg-6" v-model="gender" />
      </div>
      <div class="row">
        <locale-select class="col-lg-6" placeholder="users.locale.placeholder" required v-model="locale" />
        <TimeZoneSelect class="col-lg-6" v-model="timeZone" />
      </div>
      <PictureInput v-model="picture" />
      <ProfileInput v-model="profile" />
      <WebsiteInput v-model="website" />
    </form>
  </div>
</template>
