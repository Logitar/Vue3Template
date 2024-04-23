<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";
import { computed, ref, watch } from "vue";
import { stringUtils } from "logitar-js";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";

import AddressCountrySelect from "./AddressCountrySelect.vue";
import AddressLocalityInput from "./AddressLocalityInput.vue";
import AddressPostalCodeInput from "./AddressPostalCodeInput.vue";
import AddressRegionSelect from "./AddressRegionSelect.vue";
import AddressStreetTextarea from "./AddressStreetTextarea.vue";
import AppSaveButton from "@/components/shared/AppSaveButton.vue";
import EmailAddressInput from "./EmailAddressInput.vue";
import PhoneExtensionInput from "./PhoneExtensionInput.vue";
import PhoneNumberInput from "./PhoneNumberInput.vue";
import countries from "@/resources/countries.json";
import type { AddressPayload, EmailPayload, PhonePayload } from "@/types/users";
import type { CountrySettings } from "@/types/settings";
import type { UserProfile } from "@/types/account";
import { saveProfile } from "@/api/account";

const { isNullOrWhiteSpace } = stringUtils;
const { t } = useI18n();

const props = defineProps<{
  user: UserProfile;
}>();

const address = ref<AddressPayload>({ street: "", locality: "", postalCode: "", region: "", country: "", isVerified: false });
const country = ref<CountrySettings>();
const email = ref<EmailPayload>({ address: "", isVerified: false });
const phone = ref<PhonePayload>({ number: "", extension: "", isVerified: false });

const hasChanges = computed<boolean>(() => {
  const user: UserProfile = props.user;
  return (
    address.value.street !== (user.address?.street ?? "") ||
    address.value.locality !== (user.address?.locality ?? "") ||
    (address.value.postalCode ?? "") !== (user.address?.postalCode ?? "") ||
    address.value.country !== (user.address?.country ?? "") ||
    (address.value.region ?? "") !== (user.address?.region ?? "") ||
    email.value.address !== (user.email?.address ?? "") ||
    phone.value.number !== (user.phone?.number ?? "") ||
    (phone.value.extension ?? "") !== (user.phone?.extension ?? "")
  );
});
const isAddressRequired = computed<boolean>(() =>
  Boolean(address.value.street || address.value.locality || address.value.postalCode || address.value.country || address.value.region),
);

const emit = defineEmits<{
  (e: "error", value: unknown): void;
  (e: "saved", value: UserProfile): void;
}>();

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    const user: UserProfile = await saveProfile({
      contactInformation: {
        address: isAddressRequired.value ? address.value : undefined,
        email: email.value,
        phone: phone.value.number
          ? {
              countryCode: phone.value.countryCode,
              number: phone.value.number,
              extension: isNullOrWhiteSpace(phone.value.extension) ? undefined : phone.value.extension,
              isVerified: false,
            }
          : undefined,
      },
    });
    emit("saved", user);
  } catch (e: unknown) {
    emit("error", e);
  }
});

function clearAddress(): void {
  address.value = {
    street: "",
    locality: "",
    postalCode: "",
    region: "",
    country: "",
    isVerified: false,
  };
  country.value = undefined;
}

watch(
  props.user,
  (user) => {
    address.value = {
      street: user.address?.street ?? "",
      locality: user.address?.locality ?? "",
      postalCode: user.address?.postalCode ?? "",
      region: user.address?.region ?? "",
      country: user.address?.country ?? "",
      isVerified: false,
    };
    country.value = address.value.country ? countries.find(({ code }) => code === address.value.country) : undefined;
    email.value = {
      address: user.email?.address ?? "",
      isVerified: false,
    };
    phone.value = {
      number: user.phone?.number ?? "",
      extension: user.phone?.extension ?? "",
      isVerified: false,
    };
  },
  { deep: true, immediate: true },
);
</script>

<template>
  <form @submit.prevent="onSubmit">
    <div class="mb-3">
      <AppSaveButton :disabled="isSubmitting || !hasChanges" exists :loading="isSubmitting" />
    </div>
    <EmailAddressInput required v-model="email.address" />
    <div class="row">
      <PhoneNumberInput class="col-lg-6" :required="Boolean(phone.extension)" v-model="phone.number" />
      <PhoneExtensionInput class="col-lg-6" v-model="phone.extension" />
    </div>
    <h5>{{ t("users.address.title") }}</h5>
    <AddressStreetTextarea :required="isAddressRequired" v-model="address.street" />
    <div class="row">
      <AddressLocalityInput class="col-lg-6" :required="isAddressRequired" v-model="address.locality" />
      <AddressPostalCodeInput class="col-lg-6" :country="country" :required="Boolean(country?.postalCode)" v-model="address.postalCode" />
    </div>
    <div class="row">
      <AddressCountrySelect class="col-lg-6" :required="isAddressRequired" v-model="address.country" @selected="country = $event" />
      <AddressRegionSelect class="col-lg-6" :country="country" :required="Boolean(country?.regions?.length)" v-model="address.region" />
    </div>
    <div class="mb-3">
      <TarButton :disabled="!isAddressRequired" icon="fas fa-times" :text="t('actions.clear')" variant="warning" @click="clearAddress" />
    </div>
  </form>
</template>
