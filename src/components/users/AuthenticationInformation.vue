<script setup lang="ts">
import { TarAlert, TarButton } from "logitar-vue3-ui";
import { computed, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";

import PasswordInput from "./PasswordInput.vue";
import UsernameInput from "./UsernameInput.vue";
import type { ApiError, Error } from "@/types/api";
import type { PasswordSettings } from "@/types/settings";
import type { UserProfile } from "@/types/account";
import { saveProfile } from "@/api/account";

const passwordSettings: PasswordSettings = {
  minimumLength: 8,
  uniqueCharacters: 8,
  requireNonAlphanumeric: true,
  requireLowercase: true,
  requireUppercase: true,
  requireDigit: true,
};
const { d, t } = useI18n();

defineProps<{
  user: UserProfile;
}>();

const currentPassword = ref<string>("");
const currentPasswordRef = ref<InstanceType<typeof PasswordInput> | null>();
const invalidCredentials = ref<boolean>(false);
const newPassword = ref<string>("");
const passwordConfirmation = ref<string>("");

const hasChanges = computed<boolean>(() => Boolean(currentPassword.value || newPassword.value || passwordConfirmation.value));

const emit = defineEmits<{
  (e: "error", value: unknown): void;
  (e: "saved", value: UserProfile): void;
}>();

function reset(): void {
  currentPassword.value = "";
  newPassword.value = "";
  passwordConfirmation.value = "";
  currentPasswordRef.value?.focus();
}

const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async () => {
  try {
    invalidCredentials.value = false;
    const user: UserProfile = await saveProfile({
      authenticationInformation: {
        password: {
          current: currentPassword.value,
          new: newPassword.value,
        },
      },
    });
    reset();
    emit("saved", user);
  } catch (e: unknown) {
    const { data, status } = e as ApiError;
    if (status === 400 && (data as Error)?.code === "InvalidCredentials") {
      invalidCredentials.value = true;
      reset();
    } else {
      emit("error", e);
    }
  }
});
</script>

<template>
  <form @submit.prevent="onSubmit">
    <UsernameInput disabled floating id="username" label="users.username" :model-value="user.username" no-status placeholder="users.username" />
    <h5>{{ t("users.password.label") }}</h5>
    <TarAlert :close="t('actions.close')" dismissible variant="warning" v-model="invalidCredentials">
      <strong>{{ t("users.password.failed") }}</strong> {{ t("users.password.invalid") }}
    </TarAlert>
    <p v-if="user.passwordChangedOn">{{ t("users.password.changedOn", { date: d(new Date(user.passwordChangedOn), "medium") }) }}</p>
    <PasswordInput id="current-password" label="users.password.current" ref="currentPasswordRef" :required="hasChanges" v-model="currentPassword" />
    <div class="row">
      <PasswordInput class="col-lg-6" id="new-password" label="users.password.new" :required="hasChanges" :settings="passwordSettings" v-model="newPassword" />
      <PasswordInput
        class="col-lg-6"
        :confirm="{ value: newPassword, label: 'users.password.new' }"
        id="password-confirmation"
        label="users.password.confirm"
        :required="hasChanges"
        v-model="passwordConfirmation"
      />
    </div>
    <div class="mb-3">
      <TarButton
        :disabled="isSubmitting || !hasChanges"
        icon="fas fa-key"
        :loading="isSubmitting"
        :status="t('loading')"
        :text="t('users.password.submit')"
        type="submit"
      />
    </div>
  </form>
</template>
