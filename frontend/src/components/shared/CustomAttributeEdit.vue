<script setup lang="ts">
import type { CustomAttribute } from "@/types/CustomAttribute";

const props = defineProps<{
  attribute: CustomAttribute;
  id: string;
}>();

const emit = defineEmits<{
  (e: "remove"): void;
  (e: "update", attribute: CustomAttribute): void;
}>();
function onUpdate(changes: object): void {
  const attribute: any = { ...props.attribute };
  for (const [key, value] of Object.entries(changes)) {
    attribute[key] = value;
  }
  emit("update", attribute);
}
</script>

<template>
  <div>
    <div class="col">
      <form-input
        :id="`${id}_key`"
        label="customAttributes.key.label"
        :maxLength="255"
        :modelValue="attribute.key"
        noLabel
        placeholder="customAttributes.key.placeholder"
        required
        :rules="{ identifier: true }"
        @update:modelValue="onUpdate({ key: $event })"
      >
        <template #prepend>
          <icon-button icon="times" variant="danger" @click="$emit('remove')" />
        </template>
      </form-input>
    </div>
    <form-input
      class="col"
      :id="`${id}_value`"
      label="customAttributes.value.label"
      :modelValue="attribute.value"
      noLabel
      placeholder="customAttributes.value.placeholder"
      required
      @update:modelValue="onUpdate({ value: $event })"
    />
  </div>
</template>
