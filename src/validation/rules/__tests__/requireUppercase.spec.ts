import { describe, it, expect } from "vitest";

import requireUppercase from "../requireUppercase";

describe("requireUppercase", () => {
  it.concurrent("should return false when the value does not contain an uppercase letter", () => {
    expect(requireUppercase()).toBe(false);
    expect(requireUppercase("")).toBe(false);
    expect(requireUppercase("   ")).toBe(false);
    expect(requireUppercase("aaaa!!11")).toBe(false);
  });

  it.concurrent("should return true when the value contains an uppercase letter", () => {
    expect(requireUppercase("AAaa!!11")).toBe(true);
    expect(requireUppercase("Test123!")).toBe(true);
  });
});
