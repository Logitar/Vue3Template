import { describe, it, expect } from "vitest";

import url from "../url";

describe("url", () => {
  it.concurrent("should return false when the value is not a valid URL", () => {
    expect(url()).toBe(false);
    expect(url("/api/maps")).toBe(false);
    expect(url("ftp://maps.google.com")).toBe(false);
  });

  it.concurrent("should return true when the value is a valid URL", () => {
    expect(url("")).toBe(true);
    expect(url("   ")).toBe(true);
    expect(url("http://www.google.com")).toBe(true);
    expect(url("https://api.google.com/maps")).toBe(true);
  });
});
