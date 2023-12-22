<script setup lang="ts">
import { parsingUtils, TarCheckbox, type CheckboxOptions } from "logitar-vue3-ui";

const options: CheckboxOptions = {
  label: "Test",
  switch: true,
};

console.log(parsingUtils.parseBoolean("TRUE   "));
</script>

<template>
  <main class="container">
    <h1>This is a home page</h1>
    <TarCheckbox v-bind="options" />
  </main>
</template>
