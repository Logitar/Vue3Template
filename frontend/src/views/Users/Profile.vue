<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import BirthdateInput from "@/components/Users/BirthdateInput.vue";
import EmailAddressInput from "@/components/Users/EmailAddressInput.vue";
import GenderSelect from "@/components/Users/GenderSelect.vue";
import LocaleSelect from "@/components/Users/LocaleSelect.vue";
import PersonNameInput from "@/components/Users/PersonNameInput.vue";
import PhoneExtensionInput from "@/components/Users/PhoneExtensionInput.vue";
import PhoneNumberInput from "@/components/Users/PhoneNumberInput.vue";
import PictureInput from "@/components/Users/PictureInput.vue";
import ProfileInput from "@/components/Users/ProfileInput.vue";
import TimeZoneSelect from "@/components/Users/TimeZoneSelect.vue";
import WebsiteInput from "@/components/Users/WebsiteInput.vue";
import type { AddressInput } from "@/types/AddressInput";
import type { EmailInput } from "@/types/EmailInput";
import type { PhoneInput } from "@/types/PhoneInput";
import type { UserProfile } from "@/types/UserProfile";
import { getProfile, saveProfile } from "@/api/account";
import { handleError, toast } from "@/helpers/errorUtils";

const { d, t } = useI18n();
const { query } = useRoute();

const address = ref<AddressInput>({ line1: "", locality: "", country: "", verify: false });
const birthdate = ref<Date>();
const confirmed = ref<boolean>(query.status === "confirmed");
const email = ref<EmailInput>({ address: "", verify: false });
const firstName = ref<string>("");
const gender = ref<string>("");
const lastName = ref<string>("");
const locale = ref<string>("");
const middleName = ref<string>("");
const nickname = ref<string>("");
const phone = ref<PhoneInput>({ number: "", verify: false });
const phoneNumberRef = ref<InstanceType<typeof PhoneNumberInput> | null>(null);
const picture = ref<string>("");
const profile = ref<string>("");
const timeZone = ref<string>("");
const user = ref<UserProfile>();
const website = ref<string>("");

function setModel(model: UserProfile): void {
  user.value = model;
  address.value = {
    line1: model.address?.line1 ?? "",
    line2: model.address?.line2,
    locality: model.address?.locality ?? "",
    postalCode: model.address?.postalCode,
    country: model.address?.country ?? "",
    region: model.address?.region,
    verify: false,
  };
  birthdate.value = model.birthdate ? new Date(model.birthdate) : undefined;
  email.value = {
    address: model.email.address,
    verify: false,
  };
  firstName.value = model.firstName;
  gender.value = model.gender ?? "";
  lastName.value = model.lastName;
  locale.value = model.locale;
  middleName.value = model.middleName ?? "";
  nickname.value = model.nickname ?? "";
  phone.value = {
    countryCode: model.phone?.countryCode,
    number: model.phone?.number ?? "",
    extension: model.phone?.extension,
    verify: false,
  };
  picture.value = model.picture ?? "";
  profile.value = model.profile ?? "";
  timeZone.value = model.timeZone ?? "";
  website.value = model.website ?? "";
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
  if (phoneNumberRef.value?.isValid) {
    try {
      const { data } = await saveProfile({
        address: address.value.line1 ? address.value : undefined,
        email: email.value,
        phone: phone.value.number ? phone.value : undefined,
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
      setModel(data);
      toast("success", "users.profile.updated", "success");
    } catch (e) {
      handleError(e);
    }
  }
});
</script>

<template>
  <div class="container">
    <h1>{{ t("users.profile.title") }}</h1>
    <app-alert dismissible variant="success" v-model="confirmed">
      <strong>{{ t("users.profile.welcome", { brand: t("brand") }) }}</strong> {{ t("users.profile.confirmed") }}
    </app-alert>
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
      <EmailAddressInput :disabled="user?.email.isVerified" required validate :verified="user?.email.isVerified" v-model="email.address" />
      <div class="row">
        <PhoneNumberInput
          class="col-lg-6"
          :countryCode="phone.countryCode"
          ref="phoneNumberRef"
          :required="Boolean(phone.extension)"
          :verified="user?.phone?.isVerified"
          v-model="phone.number"
          @country-code="phone.countryCode = $event"
        />
        <PhoneExtensionInput class="col-lg-6" validate v-model="phone.extension" />
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
