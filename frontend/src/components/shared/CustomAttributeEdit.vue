<script setup lang="ts">
import type { CustomAttribute } from "@/types/_customAttribute";
import { assign } from "@/helpers/objectUtils";

const props = defineProps<{
  customAttribute: CustomAttribute;
  id: string;
}>();

const emit = defineEmits<{
  (e: "remove"): void;
  (e: "update", attribute: CustomAttribute): void;
}>();
function onUpdate(changes: object): void {
  const customAttribute: CustomAttribute = { ...props.customAttribute };
  for (const [key, value] of Object.entries(changes)) {
    assign(customAttribute, key as keyof CustomAttribute, value);
  }
  emit("update", customAttribute);
}
</script>

<template>
  <div>
    <div class="col">
      <form-input
        :id="`${id}_key`"
        label="customAttributes.key.label"
        :max-length="255"
        :model-value="customAttribute.key"
        no-label
        placeholder="customAttributes.key.placeholder"
        required
        :rules="{ identifier: true }"
        @update:model-value="onUpdate({ key: $event })"
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
      :model-value="customAttribute.value"
      no-label
      placeholder="customAttributes.value.placeholder"
      required
      @update:model-value="onUpdate({ value: $event })"
    />
  </div>
</template>
