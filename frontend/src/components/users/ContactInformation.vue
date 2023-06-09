<script setup lang="ts">
import { computed, inject, ref, watchEffect } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import AddressLineInput from "./AddressLineInput.vue";
import AddressLocalityInput from "./AddressLocalityInput.vue";
import CountrySelect from "./CountrySelect.vue";
import EmailAddressInput from "@/components/users/EmailAddressInput.vue";
import PhoneExtensionInput from "@/components/users/PhoneExtensionInput.vue";
import PhoneNumberInput from "@/components/users/PhoneNumberInput.vue";
import PostalCodeInput from "@/components/users/PostalCodeInput.vue";
import RegionSelect from "@/components/users/RegionSelect.vue";
import type { AddressPayload, EmailPayload, PhonePayload } from "@/types/users/contact/payloads";
import type { CountrySettings } from "@/types/users/contact";
import type { ProfileUpdatedEvent, UserProfile } from "@/types/users";
import { handleErrorKey } from "@/inject/App";
import { saveContactInformation } from "@/api/account";

const { t } = useI18n();
const handleError = inject(handleErrorKey) as (e: unknown) => void;

const props = defineProps<{
  user: UserProfile;
}>();

const address = ref<AddressPayload>({ line1: "", locality: "", country: "", verify: false });
const email = ref<EmailPayload>({ address: "", verify: false });
const phone = ref<PhonePayload>({ number: "", verify: false });
const phoneNumberRef = ref<InstanceType<typeof PhoneNumberInput> | null>(null);
const selectedCountry = ref<CountrySettings>();

const hasChanges = computed<boolean>(() => {
  const user = props.user;
  return (
    address.value.line1 !== (user.address?.line1 ?? "") ||
    address.value.line2 !== user.address?.line2 ||
    address.value.locality !== (user.address?.locality ?? "") ||
    address.value.postalCode !== user.address?.postalCode ||
    address.value.country !== (user.address?.country ?? "") ||
    address.value.region !== user.address?.region ||
    email.value.address !== user.email.address ||
    phone.value.countryCode !== user.phone?.countryCode ||
    phone.value.number !== (user.phone?.number ?? "") ||
    phone.value.extension !== user.phone?.extension
  );
});

const isAddressRequired = computed<boolean>(() => {
  return (
    Boolean(address.value.line1) ||
    Boolean(address.value.line2) ||
    Boolean(address.value.locality) ||
    Boolean(address.value.postalCode) ||
    Boolean(address.value.country) ||
    Boolean(address.value.region)
  );
});

watchEffect(() => {
  const user = props.user;
  address.value = {
    line1: user.address?.line1 ?? "",
    line2: user.address?.line2,
    locality: user.address?.locality ?? "",
    postalCode: user.address?.postalCode,
    country: user.address?.country ?? "",
    region: user.address?.region,
    verify: false,
  };
  email.value = {
    address: user.email.address,
    verify: false,
  };
  phone.value = {
    countryCode: user.phone?.countryCode,
    number: user.phone?.number ?? "",
    extension: user.phone?.extension,
    verify: false,
  };
});

const emit = defineEmits<{
  (e: "profile-updated", event: ProfileUpdatedEvent): void;
}>();
const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    const user = await saveContactInformation({
      address: address.value.line1 ? address.value : undefined,
      email: email.value,
      phone: phone.value.number ? phone.value : undefined,
    });
    emit("profile-updated", { user });
  } catch (e: unknown) {
    handleError(e);
  }
});

function clearAddress(): void {
  address.value.line1 = "";
  address.value.line2 = undefined;
  address.value.locality = "";
  address.value.postalCode = undefined;
  address.value.country = "";
  address.value.region = undefined;
}
</script>

<template>
  <div>
    <form @submit.prevent="onSubmit">
      <div class="mb-3">
        <icon-submit :disabled="!hasChanges || isSubmitting" icon="fas fa-floppy-disk" :loading="isSubmitting" text="actions.save" />
      </div>
      <EmailAddressInput :disabled="user.email.isVerified" required validate :verified="user.email.isVerified" v-model="email.address" />
      <div class="row">
        <PhoneNumberInput
          class="col-lg-6"
          :country-code="phone.countryCode"
          :disabled="user.phone?.isVerified"
          ref="phoneNumberRef"
          :required="Boolean(phone.extension)"
          :verified="user.phone?.isVerified"
          v-model="phone.number"
          @country-code="phone.countryCode = $event"
        />
        <PhoneExtensionInput class="col-lg-6" :disabled="user.phone?.isVerified" validate v-model="phone.extension" />
      </div>
      <h5>
        {{ t("users.address.title") }}
        <template v-if="user.address?.isVerified">
          <app-badge>{{ t("users.address.verified") }}</app-badge>
        </template>
      </h5>
      <div class="row">
        <AddressLineInput class="col-lg-6" :disabled="user.address?.isVerified" :required="isAddressRequired" type="street" validate v-model="address.line1" />
        <AddressLineInput class="col-lg-6" :disabled="user.address?.isVerified" type="additional" validate v-model="address.line2" />
      </div>
      <div class="row">
        <AddressLocalityInput class="col-lg-6" :disabled="user.address?.isVerified" :required="isAddressRequired" validate v-model="address.locality" />
        <PostalCodeInput class="col-lg-6" :country="selectedCountry" :disabled="user.address?.isVerified" validate v-model="address.postalCode" />
      </div>
      <div class="row">
        <CountrySelect
          class="col-lg-6"
          :disabled="user.address?.isVerified"
          :required="isAddressRequired"
          v-model="address.country"
          @country-selected="selectedCountry = $event"
        />
        <RegionSelect class="col-lg-6" :country="selectedCountry" :disabled="user.address?.isVerified" v-model="address.region" />
      </div>
      <icon-button
        v-if="user.address?.isVerified !== true"
        :disabled="!isAddressRequired"
        icon="fas fa-times"
        text="actions.clear"
        variant="warning"
        @click="clearAddress"
      />
    </form>
  </div>
</template>
