import { describe, it, expect } from "vitest";

import requireLowercase from "../requireLowercase";

describe("requireLowercase", () => {
  it.concurrent("should return false when the value does not contain a lowercase letter", () => {
    expect(requireLowercase()).toBe(false);
    expect(requireLowercase("")).toBe(false);
    expect(requireLowercase("   ")).toBe(false);
    expect(requireLowercase("AAAA!!11")).toBe(false);
  });

  it.concurrent("should return true when the value contains a lowercase letter", () => {
    expect(requireLowercase("AAaa!!11")).toBe(true);
    expect(requireLowercase("Test123!")).toBe(true);
  });
});
