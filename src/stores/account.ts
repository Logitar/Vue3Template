import { defineStore } from "pinia";
import { ref } from "vue";

import type { Actor } from "@/types/aggregate";

export const useAccountStore = defineStore(
  "account",
  () => {
    const authenticated = ref<Actor>();

    function signIn(user: Actor): void {
      authenticated.value = user;
    }
    function signOut(): void {
      authenticated.value = undefined;
    }

    return { authenticated, signIn, signOut };
  },
  { persist: true },
);
