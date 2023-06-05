<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { useCounterStore } from "@/stores/counter";

const { t } = useI18n();

const counter = useCounterStore();
</script>

<template>
  <main class="container">
    <h1>{{ t("home.title") }}</h1>
    <icon-button icon="fas fa-plus" @click="counter.increment">{{ " " }} {{ t("home.clickMe", { count: counter.count }) }}</icon-button>
    <p>doubleCount: {{ counter.doubleCount }}</p>
  </main>
</template>
