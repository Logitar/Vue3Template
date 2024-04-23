import { describe, it, expect } from "vitest";

import allowedCharacters from "../allowedCharacters";

const allowedCharacterList: string = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

describe("allowedCharacters", () => {
  it.concurrent("should return false when the value contains characters that are not allowed", () => {
    expect(allowedCharacters(undefined, [undefined as any])).toBe(false);
    expect(allowedCharacters(undefined, [""])).toBe(false);
    expect(allowedCharacters(undefined, [allowedCharacterList])).toBe(false);
    expect(allowedCharacters("test!", [allowedCharacterList])).toBe(false);
    expect(allowedCharacters(" test", [allowedCharacterList])).toBe(false);
  });

  it.concurrent("should return true when the value only contains allowed characters", () => {
    expect(allowedCharacters("carluiz", [undefined as any])).toBe(true);
    expect(allowedCharacters("carluiz", [""])).toBe(true);
    expect(allowedCharacters("carluiz", [allowedCharacterList])).toBe(true);
    expect(allowedCharacters("carlos.luiz@test.com", [allowedCharacterList])).toBe(true);
  });
});
