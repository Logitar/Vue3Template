<script setup lang="ts">
import DateTimeInput from "@/components/shared/DateTimeInput.vue";

const max = new Date();
const min = new Date(max);
min.setFullYear(min.getFullYear() - 100);

defineProps<{
  modelValue?: Date;
}>();

defineEmits<{
  (e: "update:model-value", value?: Date): void;
}>();
</script>

<template>
  <DateTimeInput
    floating
    id="birthdate"
    label="users.birthdate"
    :min="min"
    :max="max"
    :model-value="modelValue"
    @update:model-value="$emit('update:model-value', $event)"
  />
</template>
