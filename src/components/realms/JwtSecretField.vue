<script setup lang="ts">
import { ref } from "vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

withDefaults(
  defineProps<{
    id?: string;
    label?: string;
    modelValue?: string;
    oldValue?: string;
    placeholder?: string;
    validate?: boolean;
    warning?: string;
  }>(),
  {
    id: "secret",
    label: "realms.jwt.secret.label",
    placeholder: "realms.jwt.secret.placeholder",
    warning: "realms.jwt.secret.warning",
  }
);

const showSecret = ref<boolean>(false);

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <div>
    <form-input
      :id="id"
      :label="label"
      :max-length="validate ? 512 / 8 : undefined"
      :min-length="validate ? 256 / 8 : undefined"
      :model-value="modelValue"
      :placeholder="placeholder"
      :type="showSecret ? 'text' : 'password'"
      @update:model-value="$emit('update:model-value', $event)"
    >
      <template #append>
        <icon-button :icon="showSecret ? 'eye-slash' : 'eye'" variant="info" @click="showSecret = !showSecret" />
        <icon-button :disabled="!modelValue" icon="times" text="actions.clear" variant="warning" @click="$emit('update:model-value', '')" />
      </template>
    </form-input>
    <app-alert :show="oldValue && oldValue !== modelValue" variant="warning">
      <p>
        <strong>{{ t(warning) }}</strong>
      </p>
      <icon-button icon="fa-history" text="actions.revert" variant="warning" @click="$emit('update:model-value', oldValue)" />
    </app-alert>
  </div>
</template>
