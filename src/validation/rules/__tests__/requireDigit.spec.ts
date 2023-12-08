import { describe, it, expect } from "vitest";

import requireDigit from "../requireDigit";

describe("requireDigit", () => {
  it.concurrent("should return false when the value does not contain a digit", () => {
    expect(requireDigit()).toBe(false);
    expect(requireDigit("")).toBe(false);
    expect(requireDigit("   ")).toBe(false);
    expect(requireDigit("AAaa!!!!")).toBe(false);
  });

  it.concurrent("should return true when the value contains a digit", () => {
    expect(requireDigit("AAaa!!11")).toBe(true);
    expect(requireDigit("Test123!")).toBe(true);
  });
});
