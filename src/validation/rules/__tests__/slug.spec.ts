import { describe, it, expect } from "vitest";

import slug from "../slug";

describe("slug", () => {
  it.concurrent("should return false when the value is not a valid slug", () => {
    expect(slug()).toBe(false);
    expect(slug("-")).toBe(false);
    expect(slug("hello-world-!")).toBe(false);
  });

  it.concurrent("should return true when the value is a valid slug", () => {
    expect(slug("helloworld123")).toBe(true);
    expect(slug("hello-world-123")).toBe(true);
  });
});
