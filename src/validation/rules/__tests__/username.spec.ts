import { describe, it, expect } from "vitest";

import username from "../username";

const allowedCharacters: string = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

describe("username", () => {
  it.concurrent("should return false when the value contains characters that are not allowed", () => {
    expect(username(undefined, [undefined as any])).toBe(false);
    expect(username(undefined, [""])).toBe(false);
    expect(username(undefined, [allowedCharacters])).toBe(false);
    expect(username("test!", [allowedCharacters])).toBe(false);
    expect(username(" test", [allowedCharacters])).toBe(false);
  });

  it.concurrent("should return true when the value only contains allowed characters", () => {
    expect(username("carluiz", [undefined as any])).toBe(true);
    expect(username("carluiz", [""])).toBe(true);
    expect(username("carluiz", [allowedCharacters])).toBe(true);
    expect(username("carlos.luiz@test.com", [allowedCharacters])).toBe(true);
  });
});
