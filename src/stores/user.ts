import { defineStore } from "pinia";
import { nanoid } from "nanoid";
import { ref } from "vue";

import type { Actor } from "@/types/aggregate";
import type { RegisterPayload, SignInPayload } from "@/types/account";
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

function toActor(user: User): Actor {
  const { id, username, fullName, email, picture } = user;
  return {
    id,
    type: "User",
    isDeleted: false,
    displayName: fullName ?? username,
    emailAddress: email?.address,
    pictureUrl: picture,
  };
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

    function signIn(payload: SignInPayload): Actor {
      // Try finding the user by username
      const usernameNormalized = payload.username.trim().toUpperCase();
      let id: string | undefined = usernameIndex.value.get(usernameNormalized);

      // Try finding the user by email address
      if (!id) {
        id = emailIndex.value.get(usernameNormalized);
      }

      // Checking credentials
      if (!id) {
        throw "INVALID_CREDENTIALS"; // TODO(fpion): use error object
      }
      const user: User | undefined = users.value.get(id);
      if (!user || user.isDisabled) {
        throw "INVALID_CREDENTIALS"; // TODO(fpion): use error object
      }
      if (user.hasPassword) {
        const password: string | undefined = passwordHashes.value.get(id);
        if (!password || !payload.password || hash(payload.password) !== password) {
          throw "INVALID_CREDENTIALS"; // TODO(fpion): use error object
        }
      }

      // Authenticate the user
      user.authenticatedOn = new Date().toISOString();
      users.value.set(id, user);

      return toActor(user);
    }

    function verifyEmail(emailAddress: string): Actor | undefined {
      // Find the user identifier using the email address
      const emailAddressNormalized: string = emailAddress.trim().toUpperCase();
      const id: string | undefined = emailIndex.value.get(emailAddressNormalized);
      if (!id) {
        return;
      }

      // Find the user
      const user: User | undefined = users.value.get(id);
      if (!user?.email) {
        return;
      }

      // Check the user email is not already verified
      if (user.email.isVerified) {
        return;
      }

      // Create an actor from the user
      const actor: Actor = toActor(user);

      // Verify the user email
      user.email.isVerified = true;
      user.email.verifiedBy = actor;
      user.email.verifiedOn = new Date().toISOString();

      // Confirm the user
      user.isConfirmed = true;

      return actor;
    }

    return { create, signIn, verifyEmail };
  },
  { persist: true }, // TODO(fpion): does not seem to work because Map<K,V> is not serializable
);
