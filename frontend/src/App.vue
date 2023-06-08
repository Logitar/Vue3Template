<script setup lang="ts">
import { RouterView } from "vue-router";
import AppFooter from "./components/Layout/AppFooter.vue";
import AppNavbar from "./components/Layout/AppNavbar.vue";
</script>

<template>
  <AppNavbar environment="Development" />
  <RouterView />
  <AppFooter version="1.0.0" />
</template>
