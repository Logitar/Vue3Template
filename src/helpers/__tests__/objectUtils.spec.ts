import { describe, it, expect } from "vitest";

import { assign, isEmpty } from "../objectUtils";

type User = {
  name: string;
  email?: string;
};

describe("objectUtils.assign", () => {
  it.concurrent("should assign a value to a key", () => {
    const user: User = { name: "Charles Luiz" };
    assign(user, "name", "Carlos Luyz");
    expect(user.name).toBe("Carlos Luyz");
  });

  it.concurrent("should assign a value to an optional key", () => {
    const user: User = { name: "Charles Luiz" };
    assign(user, "email", "carlosluyz@gmail.com");
    expect(user.email).toBe("carlosluyz@gmail.com");
  });

  it.concurrent("should assign undefined to an optional key", () => {
    const user: User = { name: "Charles Luis", email: "carlosluyz@gmail.com" };
    assign(user, "email", undefined);
    expect(user.email).toBeUndefined();
  });
});

describe("objectUtils.isEmpty", () => {
  it.concurrent("should return false if the object has at least one key", () => {
    const user: User = { name: "Charles Luiz" };
    expect(isEmpty(user)).toBe(false);
    user.email = "carlosluyz@gmail.com";
    expect(isEmpty(user)).toBe(false);
  });

  it.concurrent("should return true if the object has no key", () => {
    const obj: any = {};
    expect(isEmpty(obj)).toBe(true);
    obj.key = true;
    delete obj.key;
    expect(isEmpty(obj)).toBe(true);
  });
});
