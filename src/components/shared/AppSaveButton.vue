<script setup lang="ts">
import { TarButton } from "logitar-vue3-ui";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

defineProps<{
  disabled?: boolean;
  exists?: boolean;
  loading?: boolean;
}>();
</script>

<template>
  <TarButton
    :disabled="disabled"
    :icon="exists ? 'fas fa-save' : 'fas fa-plus'"
    :loading="loading"
    :status="t('loading')"
    :text="t(exists ? 'actions.save' : 'actions.create')"
    type="submit"
    :variant="exists ? 'primary' : 'success'"
  />
</template>
