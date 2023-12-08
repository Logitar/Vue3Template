import { describe, it, expect } from "vitest";

import { combineURL, isAbsoluteURL, isDigit, isLetter, isLetterOrDigit, shortify, slugify, unaccent } from "../stringUtils";

describe("stringUtils.combineURL", () => {
  it.concurrent("should combine the segments with a slash (/)", () => {
    expect(combineURL()).toBe("/");
    expect(combineURL("Hello World!")).toBe("/Hello World!");
    expect(combineURL("hello", "world")).toBe("/hello/world");
    expect(combineURL("http://api.google.com", "users/123")).toBe("http://api.google.com/users/123");
    expect(combineURL("https://api.google.com", "users", "123")).toBe("https://api.google.com/users/123");
  });

  it.concurrent("should ignore empty values", () => {
    expect(combineURL("")).toBe("/");
    expect(combineURL("   ")).toBe("/");
    expect(combineURL("", "api", "user")).toBe("/api/user");
    expect(combineURL("   ", "api", "user")).toBe("/api/user");
    expect(combineURL("api", "", "user")).toBe("/api/user");
    expect(combineURL("api", "   ", "user")).toBe("/api/user");
    expect(combineURL("api", "user", "")).toBe("/api/user");
    expect(combineURL("api", "user", "   ")).toBe("/api/user");
  });

  it.concurrent("should ignore undefined values", () => {
    const u: any = undefined;
    expect(combineURL(u)).toBe("/");
    expect(combineURL(u, "api", "user")).toBe("/api/user");
    expect(combineURL("api", u, "user")).toBe("/api/user");
    expect(combineURL("api", "user", u)).toBe("/api/user");
  });

  it.concurrent("should remove starting & ending slashes (/) from segments", () => {
    expect(combineURL("/api", "users/123")).toBe("/api/users/123");
    expect(combineURL("api", "users/123/")).toBe("/api/users/123");
    expect(combineURL("/api", "users/123/")).toBe("/api/users/123");
  });
});

describe("stringUtils.isAbsoluteURL", () => {
  it.concurrent("should return false when input string is not an absolute URL", () => {
    expect(isAbsoluteURL("")).toBe(false);
    expect(isAbsoluteURL("  ")).toBe(false);
    expect(isAbsoluteURL("/api/users/123")).toBe(false);
  });

  it.concurrent("should return true when input string is an absolute URL", () => {
    expect(isAbsoluteURL("http://api.test.com/users/123")).toBe(true);
    expect(isAbsoluteURL("https://api.test.com/users/123")).toBe(true);
    expect(isAbsoluteURL("ftp://api.test.com/users/123")).toBe(true);
  });
});

describe("stringUtils.isDigit", () => {
  it.concurrent("should return false when input character is not a digit", () => {
    expect(isDigit("")).toBe(false);
    expect(isDigit("   ")).toBe(false);
    expect(isDigit("A")).toBe(false);
  });

  it.concurrent("should return true when input character is a digit", () => {
    expect(isDigit("0")).toBe(true);
    expect(isDigit("4")).toBe(true);
  });
});

describe("stringUtils.isLetter", () => {
  it.concurrent("should return false when input character is not a letter", () => {
    expect(isLetter("")).toBe(false);
    expect(isLetter("   ")).toBe(false);
    expect(isLetter("0")).toBe(false);
    expect(isLetter("|")).toBe(false);
  });

  it.concurrent("should return true when input character is a letter", () => {
    expect(isLetter("A")).toBe(true);
    expect(isLetter("Z")).toBe(true);
  });
});

describe("stringUtils.isLetterOrDigit", () => {
  it.concurrent("should return false when input character is not a letter, nor a digit", () => {
    expect(isLetterOrDigit("")).toBe(false);
    expect(isLetterOrDigit("   ")).toBe(false);
    expect(isLetterOrDigit("|")).toBe(false);
  });

  it.concurrent("should return true when input character is a letter or a digit", () => {
    expect(isLetterOrDigit("B")).toBe(true);
    expect(isLetterOrDigit("D")).toBe(true);
    expect(isLetterOrDigit("1")).toBe(true);
    expect(isLetterOrDigit("3")).toBe(true);
  });
});

describe("stringUtils.shortify", () => {
  it.concurrent("should return the same string when it is not too long", () => {
    expect(shortify("", 20)).toBe("");
    expect(shortify("   ", 20)).toBe("   ");
    expect(shortify("Hello World!", 20)).toBe("Hello World!");
  });

  it.concurrent("should return the shortified string when it is too long", () => {
    expect(shortify("", -1)).toBe("…");
    expect(shortify("   ", 2)).toBe(" …");
    expect(shortify("Hello World!", 10)).toBe("Hello Wor…");
  });
});

describe("stringUtils.slugify", () => {
  it.concurrent("should return an empty string when it is empty or undefined", () => {
    expect(slugify(undefined)).toBe("");
    expect(slugify("")).toBe("");
    expect(slugify("   ")).toBe("");
  });

  it.concurrent(
    "should separate the input string by non-alphanumeric characters, join them using hyphens (-), remove accents and return a lowercased slug",
    () => {
      expect(slugify("arc-en-ciel")).toBe("arc-en-ciel");
      expect(slugify("Héllo Wôrld!")).toBe("hello-world");
    }
  );
});

describe("stringUtils.unaccent", () => {
  it.concurrent("should return the same string when it contains no supported accents", () => {
    expect(unaccent("")).toBe("");
    expect(unaccent("   ")).toBe("   ");
    expect(unaccent("Hello World!")).toBe("Hello World!");
  });

  it.concurrent("should return the string without accents when it contains supported accents", () => {
    expect(unaccent("français")).toBe("francais");
    expect(unaccent("  Noël  ")).toBe("  Noel  ");
    expect(unaccent("Héllo Wôrld!")).toBe("Hello World!");
  });
});
