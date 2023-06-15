<script setup lang="ts">
import { ref } from "vue";
import { useI18n } from "vue-i18n";
import AppModal from "./AppModal.vue";

const { t } = useI18n();

withDefaults(
  defineProps<{
    confirm?: string;
    displayName?: string;
    id?: string;
    isLoading?: boolean;
    title: string;
  }>(),
  {
    id: "deleteModal",
    isLoading: false,
  }
);

const modalRef = ref<InstanceType<typeof AppModal> | null>(null);

function hide(): void {
  modalRef.value?.hide();
}
function show(): void {
  modalRef.value?.show();
}
function toggle(): void {
  modalRef.value?.toggle();
}
defineExpose({ hide, show, toggle });

defineEmits<{
  (e: "ok", hide: () => void): void;
}>();
</script>

<template>
  <AppModal :id="id" ref="modalRef" :title="title">
    <p v-if="confirm || displayName">
      <template v-if="confirm">{{ t(confirm) }}</template>
      <br v-if="confirm && displayName" />
      <span v-if="displayName" class="text-danger">{{ displayName }}</span>
    </p>
    <slot></slot>
    <template #footer>
      <icon-button icon="ban" text="actions.cancel" variant="secondary" @click="hide" />
      <icon-button :disabled="isLoading" icon="trash" :loading="isLoading" text="actions.delete" variant="danger" @click="$emit('ok', hide)" />
    </template>
  </AppModal>
</template>
