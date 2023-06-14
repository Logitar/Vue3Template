<script setup lang="ts">
import { computed, ref } from "vue";
import { RouterLink, useRouter } from "vue-router";
import { useI18n } from "vue-i18n";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();
const apiBaseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;

const account = useAccountStore();
const router = useRouter();

const props = defineProps<{
  environment: string;
}>();

const search = ref<string>("");

const environmentName = computed<string>(() => props.environment.toLowerCase());
const swaggerUrl = computed<string | undefined>(() =>
  environmentName.value === "development" ? [apiBaseUrl.replace(/^\/+|\/+$/g, ""), "swagger"].join("/") : undefined
);

function onSearch(): void {
  const query = { search: search.value, page: 1, count: 10 };
  search.value = "";
  router.push({ name: "RealmList", query });
}
</script>

<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
    <div class="container-fluid">
      <RouterLink :to="{ name: 'Home' }" class="navbar-brand">
        <img src="@/assets/img/logo.png" :alt="`${t('brand')} Logo`" height="32" />
        {{ t("brand") }}
        <span v-if="environmentName !== 'production'" class="badge text-bg-warning">{{ environmentName }}</span>
      </RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li v-if="swaggerUrl" class="nav-item">
            <a class="nav-link" :href="swaggerUrl" target="_blank"> <font-awesome-icon icon="fas fa-vial" /> Swagger</a>
          </li>
          <template v-if="account.authenticated">
            <li class="nav-item">
              <RouterLink :to="{ name: 'RealmList' }" class="nav-link"> <font-awesome-icon icon="fas fa-chess-rook" /> {{ t("realms.title") }} </RouterLink>
            </li>
          </template>
        </ul>

        <form v-if="account.authenticated" class="d-flex" role="search" @submit.prevent="onSearch">
          <div class="input-group">
            <input class="form-control" :placeholder="t('search.label')" type="search" v-model="search" aria-label="Search" />
            <button class="btn btn-outline-primary" :disabled="!search" type="submit"><font-awesome-icon icon="fas fa-search" /></button>
          </div>
        </form>

        <ul class="navbar-nav mb-2 mb-lg-0">
          <template v-if="account.authenticated">
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                <app-avatar
                  :displayName="account.authenticated.fullName ?? account.authenticated.username"
                  :emailAddress="account.authenticated.email?.address"
                  :size="24"
                  :url="account.authenticated.picture"
                />
              </a>
              <ul class="dropdown-menu dropdown-menu-end">
                <li>
                  <RouterLink class="dropdown-item" :to="{ name: 'Profile' }">
                    <font-awesome-icon icon="fas fa-user" /> {{ account.authenticated.fullName ?? account.authenticated.username }}
                  </RouterLink>
                </li>
                <li>
                  <RouterLink class="dropdown-item" :to="{ name: 'SignOut' }">
                    <font-awesome-icon icon="fas fa-arrow-right-from-bracket" /> {{ t("users.signOut.title") }}
                  </RouterLink>
                </li>
              </ul>
            </li>
          </template>
          <template v-else>
            <li class="nav-item">
              <RouterLink :to="{ name: 'SignIn' }" class="nav-link">
                <font-awesome-icon icon="fas fa-arrow-right-to-bracket" /> {{ t("users.signIn.title") }}
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink :to="{ name: 'Register' }" class="nav-link"> <font-awesome-icon icon="fas fa-user" /> {{ t("users.register.title") }} </RouterLink>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>
