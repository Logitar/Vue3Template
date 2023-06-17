<script setup lang="ts">
import { computed } from "vue";
import claimValueTypes from "@/resources/claimValueTypes.json";
import type { ClaimMapping } from "@/types/realms";
import type { SelectOption } from "@/types/components";
import { assign } from "@/helpers/objectUtils";
import { orderBy } from "@/helpers/arrayUtils";

const props = defineProps<{
  claimMapping: ClaimMapping;
  id: string;
}>();

const claimValueTypeOptions = computed<SelectOption[]>(() =>
  orderBy(
    claimValueTypes.map(({ id, name }) => ({ value: id, text: name })),
    "text"
  )
);

const emit = defineEmits<{
  (e: "remove"): void;
  (e: "update", claimMapping: ClaimMapping): void;
}>();
function onUpdate(changes: object): void {
  const claimMapping: ClaimMapping = { ...props.claimMapping };
  for (const [key, value] of Object.entries(changes)) {
    assign(claimMapping, key as keyof ClaimMapping, value);
  }
  emit("update", claimMapping);
}
</script>

<template>
  <div>
    <div class="col">
      <form-input
        :id="`${id}_key`"
        label="customAttributes.key.label"
        :max-length="255"
        :model-value="claimMapping.key"
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
      :id="`${id}_type`"
      label="realms.claimMappings.type.label"
      :model-value="claimMapping.type"
      no-label
      placeholder="realms.claimMappings.type.placeholder"
      required
      @update:model-value="onUpdate({ type: $event })"
    />
    <form-select
      class="col"
      :id="`${id}_valueType`"
      label="realms.claimMappings.valueType.label"
      :model-value="claimMapping.valueType"
      no-label
      :options="claimValueTypeOptions"
      placeholder="realms.claimMappings.valueType.placeholder"
      @update:model-value="onUpdate({ valueType: $event || null })"
    />
  </div>
</template>
