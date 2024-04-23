<script setup lang="ts">
import UrlInput from "../shared/UrlInput.vue";

defineProps<{
  modelValue?: string;
}>();

defineEmits<{
  (e: "update:model-value", value?: string): void;
}>();
</script>

<template>
  <UrlInput
    floating
    id="picture"
    label="users.pictureUrl"
    max="2048"
    :model-value="modelValue"
    placeholder="users.pictureUrl"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
