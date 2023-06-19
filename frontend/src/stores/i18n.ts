import { ref } from "vue";
import { defineStore } from "pinia";
import type { Locale } from "@/types/i18n";

export const useI18nStore = defineStore(
  "i18n",
  () => {
    const locale = ref<Locale>();

    function setLocale(value: Locale) {
      locale.value = value;
    }

    return { locale, setLocale };
  },
  { persist: true }
);
