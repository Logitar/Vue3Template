import { beforeEach, describe, it, expect } from "vitest";
import { setActivePinia, createPinia } from "pinia";

import type { UserProfile } from "../../types/users";
import { useAccountStore } from "../account";

const createdOn: string = new Date().toISOString();
const user: UserProfile = {
  username: "carluis",
  email: {
    address: "carlosluis@gmail.com",
    isVerified: true,
  },
  firstName: "Carlos",
  lastName: "Luis",
  fullName: "Carlos Luis",
  locale: "es-MX",
  createdOn,
  updatedOn: createdOn,
};

describe("accountStore", () => {
  beforeEach(() => {
    setActivePinia(createPinia());
  });

  it.concurrent("should be initially empty", () => {
    const account = useAccountStore();
    expect(account.authenticated).toBeUndefined();
  });

  it.concurrent("should sign-in an authenticated user", () => {
    const account = useAccountStore();
    account.signIn(user);
    expect(account.authenticated.username).toBe(user.username);
  });

  it.concurrent("should sign-out the authenticated user", () => {
    const account = useAccountStore();
    account.signIn(user);
    account.signOut();
    expect(account.authenticated).toBeUndefined();
  });
});
