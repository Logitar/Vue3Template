<script setup lang="ts">
import { TarButton, TarModal } from "logitar-vue3-ui";
import { computed, ref } from "vue";
import { nanoid } from "nanoid";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    close?: string;
    confirm: string;
    displayName: string;
    id?: string;
    loading?: boolean;
    title: string;
  }>(),
  {
    close: "actions.close",
    id: () => nanoid(),
  },
);

const modalRef = ref<InstanceType<typeof TarModal> | null>(null);

const modalId = computed<string>(() => `delete-modal_${props.id}`);

function hide(): void {
  modalRef.value?.hide();
}

defineEmits<{
  (e: "confirmed", callback: () => void): void;
}>();
</script>

<template>
  <span>
    <TarButton
      :disabled="loading"
      icon="fas fa-trash"
      :loading="loading"
      :text="t('actions.delete')"
      variant="danger"
      data-bs-toggle="modal"
      :data-bs-target="`#${modalId}`"
    />
    <TarModal :close="t(close)" :id="modalId" ref="modalRef" :title="t(title)">
      <p>
        {{ t(confirm) }}
        <br />
        <span class="text-danger">{{ displayName }}</span>
      </p>
      <template #footer>
        <TarButton icon="fas fa-ban" :text="t('actions.cancel')" variant="secondary" @click="hide" />
        <TarButton
          :disabled="loading"
          icon="fas fa-trash"
          :loading="loading"
          :status="t('loading')"
          :text="t('actions.delete')"
          variant="danger"
          @click="$emit('confirmed', hide)"
        />
      </template>
    </TarModal>
  </span>
</template>
