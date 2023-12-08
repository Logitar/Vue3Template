import { describe, it, expect } from "vitest";

import { orderBy, orderByDescending } from "../arrayUtils";

type User = {
  name: string;
  email?: string;
};

describe("arrayUtils.orderBy", () => {
  it.concurrent("should sort an empty array", () => {
    expect(orderBy([]).join(";")).toBe("");
    expect(orderBy([], "name").join(";")).toBe("");
    expect(orderBy([], undefined, true).join(";")).toBe("");
    expect(orderBy([], "name", true).join(";")).toBe("");
  });

  it.concurrent("should sort ascendingly a primitive array", () => {
    expect(orderBy([3, undefined, 1, 2]).join(",")).toBe("1,2,3,");
    expect(orderBy(["orange", "mango", "apple"], undefined, false).join("|")).toBe("apple|mango|orange");
  });

  it.concurrent("should sort ascendingly an object array by key", () => {
    const users: User[] = [{ name: "Charles" }, { name: "Christopher" }, { name: "Abby" }];
    expect(
      orderBy(users, "name")
        .map(({ name }) => name)
        .join("|")
    ).toBe("Abby|Charles|Christopher");
    expect(
      orderBy(users, "name", false)
        .map(({ name }) => name)
        .join("|")
    ).toBe("Abby|Charles|Christopher");
  });

  it.concurrent("should sort descendingly a primitive array", () => {
    expect(orderBy([3, 1, undefined, 2], undefined, true).join(",")).toBe("3,2,1,");
    expect(orderBy(["orange", "apple", "mango"], undefined, true).join("|")).toBe("orange|mango|apple");
  });

  it.concurrent("should sort descendingly an object array by key", () => {
    const users: User[] = [{ name: "Charles", email: "carlosluyz@gmail.com" }, { name: "Christopher" }, { name: "Abby", email: "xxxabbyxxx@hotmail.com" }];
    expect(
      orderBy(users, "email", true)
        .map(({ name }) => name)
        .join("|")
    ).toBe("Abby|Charles|Christopher");
  });
});

describe("arrayUtils.orderByDescending", () => {
  it.concurrent("should sort descendingly a primitive array", () => {
    expect(orderByDescending(["Acura", "Nissan", "Chevrolet"]).join(", ")).toBe("Nissan, Chevrolet, Acura");
    expect(orderByDescending([undefined, "Acura", "Nissan", "Chevrolet"]).join(", ")).toBe("Nissan, Chevrolet, Acura, ");
  });

  it.concurrent("should sort descendingly an empty array", () => {
    expect(orderByDescending([]).join("|")).toBe("");
    expect(orderByDescending([], "name").join("|")).toBe("");
  });

  it.concurrent("should sort descendingly an object array", () => {
    const users: User[] = [{ name: "Charles", email: "carlosluyz@gmail.com" }, { name: "Christopher" }, { name: "Abby", email: "xxxabbyxxx@hotmail.com" }];
    expect(
      orderByDescending(users, "email")
        .map(({ name }) => name)
        .join("|")
    ).toBe("Abby|Charles|Christopher");
  });
});
