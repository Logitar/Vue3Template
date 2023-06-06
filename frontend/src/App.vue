<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <header>
    <ul>
      <li>
        <RouterLink :to="{ name: 'Home' }">{{ t("home.title") }}</RouterLink>
      </li>
      <li>
        <RouterLink :to="{ name: 'About' }">About</RouterLink>
      </li>
      <li>
        <RouterLink :to="{ name: 'SignIn' }"><font-awesome-icon icon="fas fa-arrow-right-to-bracket" /> {{ t("users.signIn.title") }}</RouterLink>
      </li>
      <li>
        <RouterLink :to="{ name: 'Register' }"><font-awesome-icon icon="fas fa-user" /> {{ t("users.register.title") }}</RouterLink>
      </li>
    </ul>
  </header>

  <RouterView />
</template>
