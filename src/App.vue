<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
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
      <li><FontAwesomeIcon :icon="['fas', 'user']" /> Register</li>
    </ul>
  </nav>
  <RouterView />
</template>
