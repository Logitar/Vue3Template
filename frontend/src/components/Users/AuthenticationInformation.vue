<script setup lang="ts">
import { computed, ref } from "vue";
import { useForm } from "vee-validate";
import { useI18n } from "vue-i18n";
import PasswordInput from "./PasswordInput.vue";
import UsernameInput from "./UsernameInput.vue";
import type { ApiResult } from "@/types/ApiResult";
import type { UserProfile } from "@/types/UserProfile";
import { changePassword } from "@/api/account";
import { handleError } from "@/helpers/errorUtils";
import { toast } from "@/helpers/errorUtils";

const { d, t } = useI18n();

defineProps<{
  user: UserProfile;
}>();

const confirm = ref<string>("");
const current = ref<string>("");
const currentRef = ref<InstanceType<typeof PasswordInput> | null>(null);
const invalidCredentials = ref<boolean>(false);
const password = ref<string>("");

const hasChanges = computed<boolean>(() => Boolean(current.value) || Boolean(password.value) || Boolean(confirm.value));

function reset(): void {
  current.value = "";
  password.value = "";
  confirm.value = "";
}

const emit = defineEmits(["profileUpdated"]);
const { handleSubmit, isSubmitting } = useForm();
const onSubmit = handleSubmit(async (_, { resetForm }) => {
  invalidCredentials.value = false;
  try {
    const { data } = await changePassword({ current: current.value, password: password.value });
    resetForm();
    emit("profileUpdated", data);
    toast("success", "users.password.changed", "success");
  } catch (e: any) {
    reset();
    currentRef.value?.focus();
    const { data, status } = e as ApiResult;
    if (status === 400 && data?.code === "InvalidCredentials") {
      invalidCredentials.value = true;
    } else {
      handleError(e);
    }
  }
});
</script>

<template>
  <div>
    <form class="my-3" @submit.prevent="onSubmit">
      <UsernameInput disabled :modelValue="user.username" />
      <h5>{{ t("users.password.label") }}</h5>
      <app-alert dismissible variant="warning" v-model="invalidCredentials">
        <strong>{{ t("users.password.changeFailed") }}</strong> {{ t("users.password.invalidCredentials") }}
      </app-alert>
      <p>{{ t("users.password.changedOn", { date: d(user.passwordChangedOn, "medium") }) }}</p>
      <PasswordInput
        id="current"
        label="users.password.current.label"
        placeholder="users.password.current.placeholder"
        ref="currentRef"
        required
        v-model="current"
      />
      <div class="row">
        <PasswordInput class="col-lg-6" label="users.password.new.label" placeholder="users.password.new.placeholder" required validate v-model="password" />
        <PasswordInput
          class="col-lg-6"
          :confirm="{ value: password, label: 'users.password.new.label' }"
          id="confirm"
          label="users.password.confirm.label"
          placeholder="users.password.confirm.placeholder"
          required
          validate
          v-model="confirm"
        />
      </div>
      <icon-submit :disabled="!hasChanges || isSubmitting" icon="fas fa-key" :loading="isSubmitting" text="users.password.submit" />
    </form>
  </div>
</template>
