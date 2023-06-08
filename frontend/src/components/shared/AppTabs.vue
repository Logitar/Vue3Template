<script setup lang="ts">
withDefaults(
  defineProps<{
    id?: string;
  }>(),
  {
    id: "tabs",
  }
);

// TODO(fpion): enhance me
</script>

<template>
  <div>
    <ul class="nav nav-tabs" :id="id" role="tablist">
      <slot name="headers"></slot>
    </ul>
    <div class="tab-content" :id="`${id}-contents`">
      <slot name="contents"></slot>
    </div>
  </div>
</template>
