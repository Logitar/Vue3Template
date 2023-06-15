<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { Modal } from "bootstrap";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    id: string;
    noClose?: boolean;
    title?: string;
  }>(),
  {}
);

const modal = ref<Modal>();

const labelId = computed<string>(() => `${props.id}_label`);

function hide(): void {
  modal.value?.hide();
}
function show(): void {
  modal.value?.show();
}
function toggle(): void {
  modal.value?.toggle();
}
defineExpose({ hide, show, toggle });

onMounted(() => {
  modal.value = new Modal(`#${props.id}`);
});
</script>

<template>
  <div class="modal fade" :id="id" tabindex="-1" :aria-labelledby="labelId" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 v-if="title" class="modal-title fs-5" :id="labelId">{{ t(title) }}</h1>
          <button v-if="!noClose" type="button" class="btn-close" data-bs-dismiss="modal" :aria-label="t('actions.close')"></button>
          <slot name="title"></slot>
        </div>
        <div class="modal-body">
          <slot></slot>
        </div>
        <div class="modal-footer">
          <slot name="footer"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
