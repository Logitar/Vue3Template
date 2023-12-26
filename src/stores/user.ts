import { defineStore } from "pinia";
import { nanoid } from "nanoid";

import UserRepository from "./userRepository";
import type { Actor } from "@/types/aggregate";
import type { ErrorDetail } from "@/types/api";
import type { RegisterPayload, ResetPasswordPayload, SignInPayload } from "@/types/account";
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

export const useUserStore = defineStore("user", () => {
  function create(payload: RegisterPayload): void {
    // Initialize storage
    const users = new UserRepository(localStorage);

    // Ensure username unicity
    const username: string = payload.username.trim();
    if (users.findByUsername(username)) {
      return;
    }

    // Ensure email address unicity
    const emailAddress: string | undefined = payload.emailAddress?.trim();
    if (emailAddress && users.findByEmail(emailAddress)) {
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
    const passwordHash: string | undefined = payload.password ? hash(payload.password) : undefined;
    users.save(user, passwordHash);
  }

  function resetPassword(payload: ResetPasswordPayload): Actor | undefined {
    // Initialize storage
    const users = new UserRepository(localStorage);

    // Try finding the user by username or email address
    const user: User | undefined = users.findByUsername(payload.token) ?? users.findByEmail(payload.token);
    if (!user) {
      return;
    }

    // Change the user password
    user.version++;
    users.save(user, hash(payload.password));

    return toActor(user);
  }

  function signIn(payload: SignInPayload): Actor {
    // Initialize storage
    const users = new UserRepository(localStorage);

    // Try finding the user by username or email address
    const user: User | undefined = users.findByUsername(payload.username) ?? users.findByEmail(payload.username);

    // Checking credentials
    if (!user || user.isDisabled) {
      throw { code: "InvalidCredentials" } as ErrorDetail;
    }
    if (user.hasPassword) {
      const password: string | undefined = users.findPassword(user);
      if (!password || !payload.password || hash(payload.password) !== password) {
        throw { code: "InvalidCredentials" } as ErrorDetail;
      }
    }

    // Authenticate the user
    user.authenticatedOn = new Date().toISOString();
    user.version++;
    users.save(user);

    return toActor(user);
  }

  function verifyEmail(emailAddress: string): Actor | undefined {
    // Initialize storage
    const users = new UserRepository(localStorage);

    // Find the user by the email address
    const user: User | undefined = users.findByEmail(emailAddress);
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
    user.version++;
    users.save(user);

    return actor;
  }

  return { create, resetPassword, signIn, verifyEmail };
});
