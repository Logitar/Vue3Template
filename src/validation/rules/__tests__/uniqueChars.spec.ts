import { describe, it, expect } from "vitest";

import uniqueChars from "../uniqueChars";

describe("uniqueChars", () => {
  it.concurrent("should return false when the value does not have enough different characters", () => {
    expect(uniqueChars(undefined, [0])).toBe(false);
    expect(uniqueChars("AAaa!!11", [8])).toBe(false);
  });

  it.concurrent("should return true when the value has enough different characters", () => {
    expect(uniqueChars("Test123!", [8])).toBe(true);
  });
});
