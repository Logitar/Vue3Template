<script setup lang="ts">
import { computed, inject, onMounted, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import ClaimMappingList from "@/components/realms/ClaimMappingList.vue";
import JwtSecretField from "@/components/realms/JwtSecretField.vue";
import PasswordSettingsEdit from "@/components/realms/PasswordSettingsEdit.vue";
import UsernameSettingsEdit from "@/components/realms/UsernameSettingsEdit.vue";
import type { ClaimMapping, PasswordSettings, Realm, UsernameSettings } from "@/types/realms";
import type { CustomAttribute } from "@/types/customAttribute";
import type { ToastUtils } from "@/types/components";
import { createRealm, getRealm, updateRealm } from "@/api/realms";
import { handleErrorKey, registerTooltipsKey, toastsKey } from "@/inject/App";

const { t } = useI18n();
const route = useRoute();
const router = useRouter();
const handleError = inject(handleErrorKey) as (e: unknown) => void;
const registerTooltips = inject(registerTooltipsKey) as () => void;
const toasts = inject(toastsKey) as ToastUtils;

const defaults = {
  uniqueName: "",
  displayName: "",
  defaultLocale: "",
  url: "",
  description: "",
  requireConfirmedAccount: true,
  requireUniqueEmail: true,
  usernameSettings: { allowedCharacters: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" },
  passwordSettings: {
    requiredLength: 6,
    requiredUniqueChars: 1,
    requireNonAlphanumeric: false,
    requireLowercase: true,
    requireUppercase: true,
    requireDigit: true,
  },
  secret: "",
  claimMappings: [],
  customAttributes: [],
};

const claimMappings = ref<ClaimMapping[]>(defaults.claimMappings);
const customAttributes = ref<CustomAttribute[]>(defaults.customAttributes);
const defaultLocale = ref<string>(defaults.defaultLocale);
const description = ref<string>(defaults.description);
const displayName = ref<string>(defaults.displayName);
const hasLoaded = ref<boolean>(false);
const passwordSettings = ref<PasswordSettings>(defaults.passwordSettings);
const realm = ref<Realm>();
const requireConfirmedAccount = ref<boolean>(defaults.requireConfirmedAccount);
const requireUniqueEmail = ref<boolean>(defaults.requireUniqueEmail);
const secret = ref<string>(defaults.secret);
const uniqueName = ref<string>(defaults.uniqueName);
const url = ref<string>(defaults.url);
const usernameSettings = ref<UsernameSettings>(defaults.usernameSettings);

const hasChanges = computed<boolean>(() => {
  const model = realm.value ?? defaults;
  return (
    displayName.value !== (model.displayName ?? "") ||
    uniqueName.value !== model.uniqueName ||
    defaultLocale.value !== (model.defaultLocale ?? "") ||
    url.value !== (model.url ?? "") ||
    description.value !== (model.description ?? "") ||
    requireConfirmedAccount.value !== model.requireConfirmedAccount ||
    requireUniqueEmail.value !== model.requireUniqueEmail ||
    JSON.stringify(usernameSettings.value) !== JSON.stringify(model.usernameSettings) ||
    JSON.stringify(passwordSettings.value) !== JSON.stringify(model.passwordSettings) ||
    secret.value !== model.secret ||
    JSON.stringify(claimMappings.value) !== JSON.stringify(model.claimMappings) ||
    JSON.stringify(customAttributes.value) !== JSON.stringify(model.customAttributes)
  );
});
const title = computed<string>(() => realm.value?.displayName ?? realm.value?.uniqueName ?? t("realms.title.new"));

function setModel(model: Realm): void {
  realm.value = model;
  claimMappings.value = model.claimMappings.map((item) => ({ ...item }));
  customAttributes.value = model.customAttributes.map((item) => ({ ...item }));
  defaultLocale.value = model.defaultLocale ?? "";
  description.value = model.description ?? "";
  displayName.value = model.displayName ?? "";
  passwordSettings.value = model.passwordSettings;
  requireConfirmedAccount.value = model.requireConfirmedAccount;
  requireUniqueEmail.value = model.requireUniqueEmail;
  secret.value = model.secret;
  uniqueName.value = model.uniqueName;
  url.value = model.url ?? "";
  usernameSettings.value = model.usernameSettings;
}

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    if (realm.value) {
      const updatedRealm = await updateRealm(realm.value.id, {
        displayName: displayName.value,
        description: description.value,
        defaultLocale: defaultLocale.value,
        secret: secret.value,
        url: url.value,
        requireConfirmedAccount: requireConfirmedAccount.value,
        requireUniqueEmail: requireConfirmedAccount.value,
        usernameSettings: usernameSettings.value,
        passwordSettings: passwordSettings.value,
        claimMappings: claimMappings.value,
        customAttributes: customAttributes.value,
      });
      setModel(updatedRealm);
      toasts.success("realms.updated");
    } else {
      const createdRealm = await createRealm({
        uniqueName: uniqueName.value,
        displayName: displayName.value,
        description: description.value,
        defaultLocale: defaultLocale.value,
        secret: secret.value,
        url: url.value,
        requireConfirmedAccount: requireConfirmedAccount.value,
        requireUniqueEmail: requireConfirmedAccount.value,
        usernameSettings: usernameSettings.value,
        passwordSettings: passwordSettings.value,
        claimMappings: claimMappings.value,
        customAttributes: customAttributes.value,
      });
      setModel(createdRealm);
      toasts.success("realms.created");
      router.replace({ name: "RealmEdit", params: { id: createdRealm.id } });
    }
  } catch (e: unknown) {
    handleError(e);
  }
});

onMounted(async () => {
  const id = route.params.id?.toString();
  if (id) {
    try {
      const realm = await getRealm(id);
      setModel(realm);
    } catch (e: unknown) {
      handleError(e);
    }
  }
  registerTooltips();
  hasLoaded.value = true;
});
</script>

<template>
  <main class="container">
    <h1 v-show="hasLoaded">{{ title }}</h1>
    <status-detail v-if="realm" :aggregate="realm" />
    <form v-show="hasLoaded" @submit.prevent="onSubmit">
      <div class="mb-3">
        <icon-submit
          class="me-1"
          :disabled="isSubmitting || !hasChanges"
          :icon="realm ? 'save' : 'plus'"
          :loading="isSubmitting"
          :text="realm ? 'actions.save' : 'actions.create'"
          :variant="realm ? undefined : 'success'"
        />
        <icon-button class="ms-1" icon="chevron-left" text="actions.back" :variant="hasChanges ? 'danger' : 'secondary'" @click="router.back()" />
      </div>
      <app-tabs>
        <app-tab active title="general">
          <div class="row">
            <display-name-input class="col-lg-6" validate v-model="displayName" />
            <slug-input
              :base-value="displayName"
              class="col-lg-6"
              :disabled="Boolean(realm)"
              id="uniqueName"
              label="realms.uniqueName.label"
              placeholder="realms.uniqueName.placeholder"
              :required="!realm"
              :validate="!realm"
              v-model="uniqueName"
            />
          </div>
          <div class="row">
            <locale-select class="col-lg-6" id="defaultLocale" label="realms.defaultLocale" v-model="defaultLocale" />
            <url-input class="col-lg-6" id="url" label="realms.url.label" placeholder="realms.url.placeholder" validate v-model="url" />
          </div>
          <description-textarea v-model="description" />
        </app-tab>
        <app-tab title="settings">
          <form-checkbox class="mb-3" id="requireConfirmedAccount" v-model="requireConfirmedAccount">
            <template #label>
              <span data-bs-toggle="tooltip" :data-bs-title="t('realms.requireConfirmedAccount.info')">
                {{ t("realms.requireConfirmedAccount.label") }} <font-awesome-icon icon="fas fa-circle-info" />
              </span>
            </template>
          </form-checkbox>
          <form-checkbox class="mb-3" id="requireUniqueEmail" v-model="requireUniqueEmail">
            <template #label>
              <span data-bs-toggle="tooltip" :data-bs-title="t('realms.requireUniqueEmail.info')">
                {{ t("realms.requireUniqueEmail.label") }} <font-awesome-icon icon="fas fa-circle-info" />
              </span>
            </template>
          </form-checkbox>
          <UsernameSettingsEdit v-model="usernameSettings" />
          <PasswordSettingsEdit v-model="passwordSettings" />
          <h5>{{ t("realms.jwt.title") }}</h5>
          <JwtSecretField :old-value="realm?.secret" validate v-model="secret" />
        </app-tab>
        <app-tab title="realms.claimMappings.title">
          <ClaimMappingList v-model="claimMappings" />
        </app-tab>
        <app-tab title="customAttributes.title">
          <custom-attribute-list v-model="customAttributes" />
        </app-tab>
      </app-tabs>
    </form>
  </main>
</template>
