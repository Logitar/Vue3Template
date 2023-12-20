<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { RouterView } from "vue-router";

import { useAccountStore } from "./stores/account";

const account = useAccountStore();
</script>

<template>
  <nav>
    <ul>
      <li>
        <RouterLink :to="{ name: 'Home' }">Home</RouterLink>
      </li>
      <template v-if="account.authenticated">
        <li>
          <FontAwesomeIcon :icon="['fas', 'user']" />
          <RouterLink :to="{ name: 'Profile' }">Profile</RouterLink>
        </li>
        <li>
          <FontAwesomeIcon :icon="['fas', 'right-from-bracket']" />
          <RouterLink :to="{ name: 'SignOut' }">Sign Out</RouterLink>
        </li>
      </template>
      <template v-else>
        <li>
          <FontAwesomeIcon :icon="['fas', 'right-to-bracket']" />
          <RouterLink :to="{ name: 'SignIn' }">Sign In</RouterLink>
        </li>
        <li>
          <FontAwesomeIcon :icon="['fas', 'user']" />
          <RouterLink :to="{ name: 'Register' }">Register</RouterLink>
        </li>
      </template>
    </ul>
  </nav>
  <RouterView />
</template>
