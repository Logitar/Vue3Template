import { defineStore } from "pinia";
import { ref } from "vue";

import type { UserProfile } from "@/types/users";

export const useAccountStore = defineStore(
  "account",
  () => {
    const authenticated = ref<UserProfile>();

    function signIn(user: UserProfile): void {
      authenticated.value = user;
    }
    function signOut(): void {
      authenticated.value = undefined;
    }

    return { authenticated, signIn, signOut };
  },
  {
    persist: true,
  }
);
