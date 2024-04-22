<script setup lang="ts">
import { TarBadge } from "logitar-vue3-ui";

import { computed } from "vue";
import { useI18n } from "vue-i18n";

import type { UserProfile } from "@/types/account";

const { d, t } = useI18n();

const props = defineProps<{
  user: UserProfile;
}>();

const addressLines = computed<string[]>(() => props.user.address?.formatted.split("\n") ?? []);
</script>

<template>
  <table class="table table-striped">
    <tbody>
      <tr v-if="user.fullName">
        <th scope="row">{{ t("users.names.full") }}</th>
        <td>{{ user.fullName }}</td>
      </tr>
      <tr v-if="user.email">
        <th scope="row">{{ t("users.email.address") }}</th>
        <td>
          {{ user.email.address }}
          <TarBadge v-if="user.email.isVerified">{{ t("users.email.verified") }}</TarBadge>
        </td>
      </tr>
      <tr v-if="user.phone">
        <th scope="row">{{ t("users.phone.e164") }}</th>
        <td>
          {{ user.phone.e164Formatted }}
          <TarBadge v-if="user.phone.isVerified">{{ t("users.phone.verified") }}</TarBadge>
        </td>
      </tr>
      <tr v-if="user.address">
        <th scope="row">{{ t("users.address.title") }}</th>
        <td>
          <template v-for="(line, index) in addressLines" :key="index"><br v-if="index > 0" />{{ line }}</template>
          <template v-if="user.address.isVerified">
            {{ " " }}
            <TarBadge>{{ t("users.address.verified") }}</TarBadge>
          </template>
        </td>
      </tr>
      <tr>
        <th scope="row">{{ t("users.createdOn") }}</th>
        <td>{{ d(user.createdOn, "medium") }}</td>
      </tr>
      <tr>
        <th scope="row">{{ t("users.updatedOn") }}</th>
        <td>{{ d(user.updatedOn, "medium") }}</td>
      </tr>
      <tr v-if="user.authenticatedOn">
        <th scope="row">{{ t("users.authenticatedOn") }}</th>
        <td>{{ d(user.authenticatedOn, "medium") }}</td>
      </tr>
    </tbody>
  </table>
</template>
