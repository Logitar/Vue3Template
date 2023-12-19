<script setup lang="ts">
import { RouterView } from "vue-router";
</script>

<template>
  <nav>
    <ul>
      <li>
        <RouterLink :to="{ name: 'Home' }">Home</RouterLink>
      </li>
      <li>
        <RouterLink :to="{ name: 'About' }">About</RouterLink>
      </li>
    </ul>
  </nav>
  <RouterView />
</template>
