<script setup lang="ts">
import { computed } from "vue";
import { RouterLink } from "vue-router";
import { useI18n } from "vue-i18n";
import UserAvatar from "../Users/UserAvatar.vue";
import { useAccountStore } from "@/stores/account";

const { t } = useI18n();

const account = useAccountStore();

const props = defineProps<{
  environment: string;
}>();

const environmentName = computed<string>(() => props.environment.toLowerCase());
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
          <!-- <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="#">Home</a>
          </li> -->

          <!-- <li class="nav-item">
            <a class="nav-link" href="#">Link</a>
          </li> -->

          <!-- <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"> Dropdown </a>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="#">Action</a></li>
              <li><a class="dropdown-item" href="#">Another action</a></li>
              <li><hr class="dropdown-divider" /></li>
              <li><a class="dropdown-item" href="#">Something else here</a></li>
            </ul>
          </li> -->

          <!-- <li class="nav-item">
            <a class="nav-link disabled">Disabled</a>
          </li> -->
        </ul>

        <!-- <form class="d-flex" role="search">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form> -->

        <ul class="navbar-nav mb-2 mb-lg-0">
          <template v-if="account.authenticated">
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                <UserAvatar :size="24" :user="account.authenticated" />
              </a>
              <ul class="dropdown-menu dropdown-menu-end">
                <li>
                  <RouterLink class="dropdown-item" :to="{ name: 'Profile' }">
                    <font-awesome-icon :icon="['fas', 'fa-user']" /> {{ account.authenticated.fullName }}
                  </RouterLink>
                </li>
                <li>
                  <RouterLink class="dropdown-item" :to="{ name: 'SignOut' }">
                    <font-awesome-icon :icon="['fas', 'fa-arrow-right-from-bracket']" /> {{ t("users.signOut.title") }}
                  </RouterLink>
                </li>
              </ul>
            </li>
          </template>
          <template v-else>
            <li class="nav-item">
              <RouterLink :to="{ name: 'SignIn' }" class="nav-link">
                <font-awesome-icon :icon="['fas', 'fa-arrow-right-to-bracket']" /> {{ t("users.signIn.title") }}
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink :to="{ name: 'Register' }" class="nav-link">
                <font-awesome-icon :icon="['fas', 'fa-user']" /> {{ t("users.register.title") }}
              </RouterLink>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>
