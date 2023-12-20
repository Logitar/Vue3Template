<script setup lang="ts">
import { onMounted } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

onMounted(() => {
  const token = route.params.token as string; // TODO(fpion): implement
});
</script>

<template>
  <main class="container"></main>
</template>
