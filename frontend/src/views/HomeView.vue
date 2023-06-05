<script setup lang="ts">
import { useCounterStore } from "@/stores/counter";

const counter = useCounterStore();
</script>

<template>
  <main class="container">
    <h1>{{ $t("home.title") }}</h1>
    <IconButton icon="fa-solid fa-plus" @click="counter.increment">{{ " " }} {{ $t("home.clickMe", { count: counter.count }) }}</IconButton>
    <p>doubleCount: {{ counter.doubleCount }}</p>
  </main>
</template>
