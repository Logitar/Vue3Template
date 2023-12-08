import { describe, it, expect } from "vitest";

import identifier from "../identifier";

describe("identifier", () => {
  it.concurrent("should return false when the value is not a valid identifier", () => {
    expect(identifier()).toBe(false);
    expect(identifier("")).toBe(false);
    expect(identifier("   ")).toBe(false);
    expect(identifier("0name")).toBe(false);
    expect(identifier("person-name")).toBe(false);
  });

  it.concurrent("should return true when the value is a valid identifier", () => {
    expect(identifier("_name")).toBe(true);
    expect(identifier("PersonName")).toBe(true);
  });
});
