import { beforeEach, describe, it, expect } from "vitest";
import { setActivePinia, createPinia } from "pinia";

import type { CurrentUser } from "../../types/account";
import { useAccountStore } from "../account";

const user: CurrentUser = {
  displayName: "Francis Pion",
  emailAddress: "no-reply@francispion.ca",
  pictureUrl: "https://www.francispion.ca/assets/img/profile-img.jpg",
};

describe("accountStore", () => {
  beforeEach(() => {
    setActivePinia(createPinia());
  });

  it.concurrent("should be initially empty", () => {
    const account = useAccountStore();
    expect(account.currentUser).toBeUndefined();
  });

  it.concurrent("should sign-in the current user", () => {
    const account = useAccountStore();
    account.signIn(user);
    expect(account.currentUser?.displayName).toBe(user.displayName);
    expect(account.currentUser?.emailAddress).toBe(user.emailAddress);
    expect(account.currentUser?.pictureUrl).toBe(user.pictureUrl);
  });

  it.concurrent("should sign-out the current user", () => {
    const account = useAccountStore();
    account.signIn(user);
    expect(account.currentUser?.displayName).toBe(user.displayName);
    account.signOut();
    expect(account.currentUser).toBeUndefined();
  });
});
