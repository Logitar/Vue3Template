<script setup lang="ts">
import { useCounterStore } from "@/stores/counter";

const counter = useCounterStore();
</script>

<template>
  <main class="container">
    <h1>Home</h1>
    <IconButton icon="fa-solid fa-plus" @click="counter.increment">{{ " " }} Click Me! ({{ counter.count }})</IconButton>
    <p>doubleCount: {{ counter.doubleCount }}</p>
  </main>
</template>
