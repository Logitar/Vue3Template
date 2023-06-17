<script setup lang="ts">
import { computed, ref } from "vue";
import { RouterLink, useRouter } from "vue-router";
import { useI18n } from "vue-i18n";
import { setLocale } from "@vee-validate/i18n";
import locales from "@/resources/locales.json";
import type { AuthenticatedUser } from "@/types/users";
import type { Locale } from "@/types/components";
import { orderBy } from "@/helpers/arrayUtils";
import { urlCombine } from "@/helpers/stringUtils";
import { useAccountStore } from "@/stores/account";

const { availableLocales, locale, t } = useI18n();
const apiBaseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;

const account = useAccountStore();
const router = useRouter();

const props = defineProps<{
  environment: string;
}>();

const search = ref<string>("");

const environmentName = computed<string>(() => props.environment.toLowerCase());
const swaggerUrl = computed<string | undefined>(() => (environmentName.value === "development" ? urlCombine(apiBaseUrl, "/swagger") : undefined));
const user = computed<AuthenticatedUser>(() => ({
  displayName: account.authenticated?.fullName ?? account.authenticated?.username,
  emailAddress: account.authenticated?.email.address,
  picture: account.authenticated?.picture,
}));

const currentLocale = computed<Locale | undefined>(() => {
  const currentLocale = locales.find(({ Name }) => Name === locale.value);
  return currentLocale
    ? {
        code: currentLocale.Name,
        nativeName: currentLocale.NativeName,
      }
    : undefined;
});
const otherLocales = computed<Locale[]>(() => {
  const otherLocales = new Set<string>(availableLocales.filter((item) => item !== locale.value));
  return orderBy(
    locales.filter(({ Name }) => otherLocales.has(Name)).map(({ Name, NativeName }) => ({ code: Name, nativeName: NativeName })),
    "nativeName"
  );
});

function onSearch(): void {
  const query = { search: search.value, page: 1, count: 10 };
  search.value = "";
  router.push({ name: "RealmList", query });
}

function translate({ code }: Locale): void {
  // TODO(fpion): persist in localStorage, on-load & on-save
  locale.value = code;
  setLocale(code); // TODO(fpion): doesn't seem to work on existing messages and labels
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
              <RouterLink :to="{ name: 'RealmList' }" class="nav-link">
                <font-awesome-icon icon="fas fa-chess-rook" /> {{ t("realms.title.list") }}
              </RouterLink>
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
          <template v-if="currentLocale">
            <li v-if="otherLocales.length > 1" class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">{{ currentLocale.nativeName }}</a>
              <ul class="dropdown-menu dropdown-menu-end">
                <li v-for="option in otherLocales" :key="option.code">
                  <a class="dropdown-item" href="#" @click.prevent="translate(option)">{{ option.nativeName }}</a>
                </li>
              </ul>
            </li>
            <li v-else-if="otherLocales.length === 1" class="nav-item">
              <a class="nav-link" href="#" @click.prevent="translate(otherLocales[0])">{{ otherLocales[0].nativeName }}</a>
            </li>
          </template>
          <template v-if="account.authenticated">
            <li class="nav-item d-block d-lg-none">
              <RouterLink class="nav-link" :to="{ name: 'Profile' }">
                <app-avatar :display-name="user.displayName" :email-address="user.emailAddress" :size="24" :url="user.picture" />
                {{ user.displayName }}
              </RouterLink>
            </li>
            <li class="nav-item d-block d-lg-none">
              <RouterLink class="nav-link" :to="{ name: 'SignOut' }">
                <font-awesome-icon icon="fas fa-arrow-right-from-bracket" /> {{ t("users.signOut.title") }}
              </RouterLink>
            </li>
            <li class="nav-item dropdown d-none d-lg-block">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                <app-avatar :display-name="user.displayName" :email-address="user.emailAddress" :size="24" :url="user.picture" />
              </a>
              <ul class="dropdown-menu dropdown-menu-end">
                <li>
                  <RouterLink class="dropdown-item" :to="{ name: 'Profile' }"><font-awesome-icon icon="fas fa-user" /> {{ user.displayName }}</RouterLink>
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
