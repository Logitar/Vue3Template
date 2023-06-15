<script setup lang="ts">
import { useI18n } from "vue-i18n";
import ClaimMappingEdit from "./ClaimMappingEdit.vue";
import type { ClaimMapping } from "@/types/ClaimMapping";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    id?: string;
    modelValue?: ClaimMapping[];
  }>(),
  {
    id: "claimMappings",
    modelValue: () => [],
  }
);

const emit = defineEmits<{
  (e: "update:modelValue", value: ClaimMapping[]): void;
}>();

function add(): void {
  const value = [...props.modelValue];
  value.push({ key: "", type: "" });
  emit("update:modelValue", value);
}
function remove(index: number): void {
  const value = [...props.modelValue];
  value.splice(index, 1);
  emit("update:modelValue", value);
}
function update(index: number, mapping: ClaimMapping): void {
  const value = [...props.modelValue];
  value.splice(index, 1, mapping);
  emit("update:modelValue", value);
}
</script>

<template>
  <div :id="id">
    <div class="mb-3">
      <icon-button icon="plus" text="actions.add" variant="success" @click="add" />
    </div>
    <template v-if="modelValue.length">
      <div class="row">
        <h5 class="col">{{ t("customAttributes.key.label") }}</h5>
        <h5 class="col">{{ t("realms.claimMappings.type.label") }}</h5>
        <h5 class="col">{{ t("realms.claimMappings.valueType.label") }}</h5>
      </div>
      <ClaimMappingEdit
        v-for="(claimMapping, index) in modelValue"
        :key="index"
        :claimMapping="claimMapping"
        class="row"
        :id="[id, index].join('_')"
        @remove="remove(index)"
        @update="update(index, $event)"
      />
    </template>
    <p v-else>{{ t("realms.claimMappings.empty") }}</p>
  </div>
</template>
