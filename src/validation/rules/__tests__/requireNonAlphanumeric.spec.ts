import { describe, it, expect } from "vitest";

import requireNonAlphanumeric from "../requireNonAlphanumeric";

describe("requireNonAlphanumeric", () => {
  it.concurrent("should return false when the value does not contain a non-alphanumeric character", () => {
    expect(requireNonAlphanumeric()).toBe(false);
    expect(requireNonAlphanumeric("")).toBe(false);
    expect(requireNonAlphanumeric("AAaa1111")).toBe(false);
  });

  it.concurrent("should return true when the value contains a non-alphanumeric character", () => {
    expect(requireNonAlphanumeric("AAaa!!11")).toBe(true);
    expect(requireNonAlphanumeric("Test123!")).toBe(true);
  });
});
