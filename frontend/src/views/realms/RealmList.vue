<script setup lang="ts">
import { computed, inject, ref, watch } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";
import type { Realm } from "@/types/realms";
import type { SearchParameters } from "@/types/api";
import type { SelectOption, ToastUtils } from "@/types/components";
import { handleErrorKey, toastsKey } from "@/inject/App";
import { isEmpty } from "@/helpers/objectUtils";
import { orderBy } from "@/helpers/arrayUtils";
import { deleteRealm, searchRealms } from "@/api/realms";
const { rt, t, tm } = useI18n();
const route = useRoute();
const router = useRouter();
const handleError = inject(handleErrorKey) as (e: unknown) => void;
const toasts = inject(toastsKey) as ToastUtils;

const isLoading = ref<boolean>(false);
const realms = ref<Realm[]>([]);
const timestamp = ref<number>(0);
const total = ref<number>(0);

const count = computed<number>(() => Number(route.query.count) || 10);
const isDescending = computed<boolean>(() => route.query.isDescending === "true");
const page = computed<number>(() => Number(route.query.page) || 1);
const search = computed<string>(() => route.query.search?.toString() ?? "");
const sort = computed<string>(() => route.query.sort?.toString() ?? "");

const sortOptions = computed<SelectOption[]>(() =>
  orderBy(
    Object.entries(tm(rt("realms.sort.options"))).map(([value, text]) => ({ text, value } as SelectOption)),
    "text"
  )
);

async function refresh(): Promise<void> {
  const params: SearchParameters = {
    search: {
      terms: search.value
        .split(" ")
        .filter((term) => Boolean(term))
        .map((term) => ({ value: `%${term}%` })),
      operator: "And",
    },
    sort: sort.value ? [{ field: sort.value, isDescending: isDescending.value }] : [],
    skip: (page.value - 1) * count.value,
    limit: count.value,
  };
  isLoading.value = true;
  const now = Date.now();
  timestamp.value = now;
  try {
    const { data } = await searchRealms(params);
    if (now === timestamp.value) {
      realms.value = data.results;
      total.value = data.total;
    }
  } catch (e: unknown) {
    handleError(e);
  } finally {
    if (now === timestamp.value) {
      isLoading.value = false;
    }
  }
}

async function onDelete(realm: Realm, hideModal: () => void): Promise<void> {
  if (!isLoading.value) {
    isLoading.value = true;
    try {
      await deleteRealm(realm.id);
      hideModal();
      toasts.success("realms.delete.success");
    } catch (e) {
      handleError(e);
      return;
    } finally {
      isLoading.value = false;
    }
    await refresh();
  }
}

function setQuery(key: string, value: string): void {
  const query = { ...route.query, [key]: value };
  switch (key) {
    case "search":
    case "count":
      query.page = "1";
      break;
  }
  router.replace({ ...route, query });
}

watch(
  () => route,
  (route) => {
    if (route.name === "RealmList") {
      const { query } = route;
      if (!query.page || !query.count) {
        router.replace({
          ...route,
          query: isEmpty(query)
            ? {
                search: "",
                sort: "UpdatedOn",
                isDescending: "true",
                page: 1,
                count: 10,
              }
            : {
                page: 1,
                count: 10,
                ...query,
              },
        });
      } else {
        refresh();
      }
    }
  },
  { deep: true, immediate: true }
);
</script>

<template>
  <div class="container">
    <h1>{{ t("realms.title.list") }}</h1>
    <div class="my-2">
      <icon-button class="me-1" :disabled="isLoading" icon="fas fa-rotate" :loading="isLoading" text="actions.refresh" @click="refresh()" />
      <icon-button class="ms-1" icon="fas fa-plus" text="actions.create" :to="{ name: 'CreateRealm' }" variant="success" />
    </div>
    <div class="row">
      <search-input class="col-lg-4" :model-value="search" @update:model-value="setQuery('search', $event)" />
      <sort-select
        class="col-lg-4"
        :descending="isDescending"
        :model-value="sort"
        :options="sortOptions"
        @descending="setQuery('isDescending', $event)"
        @update:model-value="setQuery('sort', $event)"
      />
      <count-select class="col-lg-4" :model-value="count" @update:model-value="setQuery('count', $event)" />
    </div>
    <template v-if="realms.length">
      <table class="table table-striped">
        <thead>
          <tr>
            <th scope="col">{{ t("realms.sort.options.UniqueName") }}</th>
            <th scope="col">{{ t("realms.sort.options.DisplayName") }}</th>
            <th scope="col">{{ t("realms.sort.options.UpdatedOn") }}</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="realm in realms" :key="realm.id">
            <td>
              <RouterLink :to="{ name: 'RealmEdit', params: { id: realm.id } }">{{ realm.uniqueName }}</RouterLink>
            </td>
            <td>{{ realm.displayName ?? "—" }}</td>
            <td><status-block :actor="realm.updatedBy" :date="realm.updatedOn" /></td>
            <td>
              <icon-button
                :disabled="isLoading"
                icon="trash"
                text="actions.delete"
                variant="danger"
                data-bs-toggle="modal"
                :data-bs-target="`#deleteModal_${realm.id}`"
              />
              <delete-modal
                confirm="realms.delete.confirm"
                :display-name="realm.displayName ? `${realm.displayName} (${realm.uniqueName})` : realm.uniqueName"
                :id="`deleteModal_${realm.id}`"
                :loading="isLoading"
                title="realms.delete.title"
                @ok="onDelete(realm, $event)"
              />
            </td>
          </tr>
        </tbody>
      </table>
      <app-pagination :count="count" :model-value="page" :total="total" @update:model-value="setQuery('page', $event)" />
    </template>
    <p v-else>{{ t("realms.empty") }}</p>
  </div>
</template>
