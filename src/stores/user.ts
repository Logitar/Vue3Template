import { defineStore } from "pinia";
import { ref } from "vue";

import type { CurrentUser, SignInPayload } from "@/types/account";

type ActorType = "ApiKey" | "System" | "User";
type Actor = {
  id: string;
  type: ActorType;
  isDeleted: boolean;
  displayName: string;
  emailAddress?: string;
  pictureUrl?: string;
};
type Contact = {
  isVerified: boolean;
  verifiedBy?: Actor;
  verifiedOn?: string;
};
type Email = Contact & {
  address: string;
};
type User = {
  uniqueName: string;
  uniqueNameNormalized: string;
  password: string;
  email?: Email;
  fullName?: string;
  picture?: string;
};

function normalize(value: string): string {
  return value.trim().toUpperCase();
}

export const useUserStore = defineStore(
  "users",
  () => {
    const users = ref<User[]>([]);

    function signIn(payload: SignInPayload): CurrentUser {
      const usernameNormalized: string = normalize(payload.username);
      let user: User | undefined = users.value.find(({ uniqueNameNormalized }) => uniqueNameNormalized === usernameNormalized);
      if (!user) {
        user = {
          uniqueName: payload.username,
          uniqueNameNormalized: usernameNormalized,
          password: payload.password,
        };
        users.value.push(user);
      } else if (user.password !== payload.password) {
        throw {
          data: { code: "InvalidCredentials", message: "", data: [] },
          status: 400,
        };
      }
      return {
        displayName: user.fullName ?? user.uniqueName,
        emailAddress: user.email?.address,
        pictureUrl: user.picture,
      };
    }

    return { users, signIn };
  },
  {
    persist: true,
  },
);
