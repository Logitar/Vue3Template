import { defineStore } from "pinia";
import { nanoid } from "nanoid";
import { ref } from "vue";

import type { Actor } from "@/types/aggregate";
import type { RegisterPayload } from "@/types/account";
import type { User } from "@/types/users";

function buildFullName(...names: (string | undefined)[]): string | undefined {
  return (
    names
      .join(" ")
      .split(" ")
      .filter((name) => name.length > 0)
      .join(" ") || undefined
  );
}

function hash(password: string): string {
  return password
    .split("")
    .map(() => "*")
    .join(""); // TODO(fpion): use a secure hashing function
}

export const useUserStore = defineStore(
  "user",
  () => {
    const emailIndex = ref<Map<string, string>>(new Map<string, string>());
    const passwordHashes = ref<Map<string, string>>(new Map<string, string>());
    const usernameIndex = ref<Map<string, string>>(new Map<string, string>());
    const users = ref<Map<string, User>>(new Map<string, User>());

    function create(payload: RegisterPayload): void {
      // Ensure username unicity
      const username: string = payload.username.trim();
      const usernameNormalized: string = username.toUpperCase();
      if (usernameIndex.value.has(usernameNormalized)) {
        return;
      }

      // Ensure email address unicity
      const emailAddress: string | undefined = payload.emailAddress?.trim();
      const emailAddressNormalized: string | undefined = emailAddress?.toUpperCase();
      if (emailAddressNormalized && emailIndex.value.has(emailAddressNormalized)) {
        return;
      }

      // Prepare user information
      const firstName: string | undefined = payload.firstName?.trim();
      const lastName: string | undefined = payload.lastName?.trim();
      const fullName: string | undefined = buildFullName(firstName, lastName);

      // Generate an unique identifier and a timestamp
      const id: string = nanoid();
      const now: string = new Date().toISOString();

      // Create an actor
      const actor: Actor = {
        id,
        type: "User",
        isDeleted: false,
        displayName: fullName ?? username,
        emailAddress,
      };

      // Create the user
      const user: User = {
        id,
        version: 1,
        createdBy: actor,
        createdOn: now,
        updatedBy: actor,
        updatedOn: now,
        username,
        hasPassword: Boolean(payload.password),
        isDisabled: false,
        isConfirmed: false,
        firstName,
        lastName,
        fullName,
      };
      if (payload.password) {
        user.passwordChangedBy = actor;
        user.passwordChangedOn = now;
      }
      if (emailAddress) {
        user.email = { isVerified: false, address: emailAddress };
      }

      // Save the user
      users.value.set(id, user);
      usernameIndex.value.set(usernameNormalized, id);
      if (emailAddressNormalized) {
        emailIndex.value.set(emailAddressNormalized, id);
      }
      if (payload.password) {
        passwordHashes.value.set(id, hash(payload.password));
      }
    }

    return { create };
  },
  { persist: true },
);
