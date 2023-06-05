<script setup lang="ts">
import { useCounterStore } from "@/stores/counter";

const counter = useCounterStore();
</script>

<template>
  <main class="container">
    <h1>Home</h1>
    <button class="btn btn-primary" @click="counter.increment">Click Me! ({{ counter.count }})</button>
    <p>doubleCount: {{ counter.doubleCount }}</p>
  </main>
</template>
