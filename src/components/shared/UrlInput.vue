<script setup lang="ts">
withDefaults(
  defineProps<{
    disabled?: boolean;
    id: string;
    label?: string;
    maxLength?: number;
    minLength?: number;
    modelValue?: string;
    name?: string;
    placeholder?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    maxLength: 2048,
    required: false,
  }
);

defineEmits<{
  (e: "update:model-value", value: string): void;
}>();
</script>

<template>
  <form-input
    :disabled="disabled"
    :id="id"
    :label="label"
    :max-length="maxLength"
    :min-length="minLength"
    :model-value="modelValue"
    :name="name"
    :placeholder="placeholder"
    :required="required"
    type="url"
    @update:model-value="$emit('update:model-value', $event)"
  >
    <template #append>
      <a :class="{ 'btn btn-info text-white': true, disabled: !modelValue }" :href="modelValue" target="_blank">
        <font-awesome-icon icon="fas fa-arrow-up-right-from-square" />
      </a>
    </template>
  </form-input>
</template>
