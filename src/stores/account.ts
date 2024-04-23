import { defineStore } from "pinia";
import { ref } from "vue";

import type { CurrentUser } from "@/types/account";
export const useAccountStore = defineStore(
  "account",
  () => {
    const currentUser = ref<CurrentUser>();

    function signIn(user: CurrentUser): void {
      currentUser.value = user;
    }
    function signOut(): void {
      currentUser.value = undefined;
    }

    return { currentUser, signIn, signOut };
  },
  {
    persist: true,
  },
);
