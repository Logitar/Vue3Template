<script setup lang="ts">
import AppInput from "@/components/shared/AppInput.vue";
import { computed } from "vue";
import type { PersonNameType } from "@/types/account";

const props = defineProps<{
  modelValue?: string;
  required?: boolean | string;
  type: PersonNameType;
}>();

const inputId = computed<string>(() => (props.type === "nick" ? "nickname" : `${props.type}-name`));
const inputLabel = computed<string>(() => `users.names.${props.type}`);

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <AppInput
    floating
    :id="inputId"
    :label="inputLabel"
    max="255"
    :model-value="modelValue"
    :placeholder="inputLabel"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
