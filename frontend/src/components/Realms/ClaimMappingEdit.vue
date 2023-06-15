<script setup lang="ts">
import { computed } from "vue";
import claimValueTypes from "@/resources/claimValueTypes.json";
import type { ClaimMapping } from "@/types/ClaimMapping";
import type { SelectOption } from "@/types/SelectOption";
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
  const claimMapping: any = { ...props.claimMapping };
  for (const [key, value] of Object.entries(changes)) {
    claimMapping[key] = value;
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
        :maxLength="255"
        :modelValue="claimMapping.key"
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
      :id="`${id}_type`"
      label="realms.claimMappings.type.label"
      :modelValue="claimMapping.type"
      noLabel
      placeholder="realms.claimMappings.type.placeholder"
      required
      @update:modelValue="onUpdate({ type: $event })"
    />
    <form-select
      class="col"
      :id="`${id}_valueType`"
      label="realms.claimMappings.valueType.label"
      :modelValue="claimMapping.valueType"
      noLabel
      :options="claimValueTypeOptions"
      placeholder="realms.claimMappings.valueType.placeholder"
      @update:modelValue="onUpdate({ valueType: $event || null })"
    />
  </div>
</template>
