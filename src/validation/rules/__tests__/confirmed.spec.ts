import { describe, it, expect } from "vitest";

import confirmed from "../confirmed";

describe("confirmed", () => {
  it.concurrent("should return false when the values are different", () => {
    expect(confirmed("password", ["P@s$W0rD"])).toBe(false);
  });

  it.concurrent("should return true when the values are the same", () => {
    expect(confirmed("Test123!", ["Test123!"])).toBe(true);
  });
});
