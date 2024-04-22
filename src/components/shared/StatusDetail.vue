<script setup lang="ts">
import StatusInfo from "./StatusInfo.vue";
import type { Aggregate } from "@/types/aggregate";

defineProps<{
  aggregate: Aggregate;
}>();
</script>

<template>
  <p>
    <StatusInfo :actor="aggregate.createdBy" :date="aggregate.createdOn" format="status.createdOn" />
    <br />
    <StatusInfo :actor="aggregate.updatedBy" :date="aggregate.updatedOn" format="status.updatedOn" />
  </p>
</template>
