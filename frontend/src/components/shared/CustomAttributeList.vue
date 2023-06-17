<script setup lang="ts">
import { useI18n } from "vue-i18n";
import CustomAttributeEdit from "./CustomAttributeEdit.vue";
import type { CustomAttribute } from "@/types/_customAttribute";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    id?: string;
    modelValue?: CustomAttribute[];
  }>(),
  {
    id: "customAttributes",
    modelValue: () => [],
  }
);

const emit = defineEmits<{
  (e: "update:model-value", value: CustomAttribute[]): void;
}>();

function add(): void {
  const value = [...props.modelValue];
  value.push({ key: "", value: "" });
  emit("update:model-value", value);
}
function remove(index: number): void {
  const value = [...props.modelValue];
  value.splice(index, 1);
  emit("update:model-value", value);
}
function update(index: number, attribute: CustomAttribute): void {
  const value = [...props.modelValue];
  value.splice(index, 1, attribute);
  emit("update:model-value", value);
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
        <h5 class="col">{{ t("customAttributes.value.label") }}</h5>
      </div>
      <CustomAttributeEdit
        v-for="(attribute, index) in modelValue"
        :key="index"
        :custom-attribute="attribute"
        class="row"
        :id="[id, index].join('_')"
        @remove="remove(index)"
        @update="update(index, $event)"
      />
    </template>
    <p v-else>{{ t("customAttributes.empty") }}</p>
  </div>
</template>
