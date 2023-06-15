<script setup lang="ts">
import type { PasswordSettings } from "@/types/PasswordSettings";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const props = defineProps<{
  modelValue: PasswordSettings;
}>();

const emit = defineEmits<{
  (e: "update:modelValue", value: PasswordSettings): void;
}>();
function onUpdate(changes: object): void {
  const settings: any = { ...props.modelValue };
  for (const [key, value] of Object.entries(changes)) {
    settings[key] = value;
  }
  emit("update:modelValue", settings);
}
</script>

<template>
  <div>
    <h5>{{ t("realms.password.title") }}</h5>
    <div class="row">
      <form-input
        class="col-lg-6"
        id="requiredLength"
        label="realms.password.requiredLength.label"
        :minValue="1"
        :modelValue="modelValue.requiredLength.toString()"
        placeholder="realms.password.requiredLength.placeholder"
        required
        type="number"
        @update:modelValue="onUpdate({ requiredLength: Number($event) })"
      />
      <form-input
        class="col-lg-6"
        id="requiredUniqueChars"
        label="realms.password.requiredUniqueChars.label"
        :minValue="1"
        :maxValue="modelValue.requiredLength"
        :modelValue="modelValue.requiredUniqueChars.toString()"
        placeholder="realms.password.requiredUniqueChars.placeholder"
        required
        type="number"
        @update:modelValue="onUpdate({ requiredUniqueChars: Number($event) })"
      />
    </div>
    <div class="mb-3">
      <form-checkbox
        id="requireLowercase"
        label="realms.password.requireLowercase"
        :modelValue="modelValue.requireLowercase"
        @update:modelValue="onUpdate({ requireLowercase: $event })"
      />
      <form-checkbox
        id="requireUppercase"
        label="realms.password.requireUppercase"
        :modelValue="modelValue.requireUppercase"
        @update:modelValue="onUpdate({ requireUppercase: $event })"
      />
      <form-checkbox
        id="requireDigit"
        label="realms.password.requireDigit"
        :modelValue="modelValue.requireDigit"
        @update:modelValue="onUpdate({ requireDigit: $event })"
      />
      <form-checkbox
        id="requireNonAlphanumeric"
        label="realms.password.requireNonAlphanumeric"
        :modelValue="modelValue.requireNonAlphanumeric"
        @update:modelValue="onUpdate({ requireNonAlphanumeric: $event })"
      />
    </div>
  </div>
</template>
