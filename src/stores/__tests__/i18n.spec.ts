import { beforeEach, describe, it, expect } from "vitest";
import { setActivePinia, createPinia } from "pinia";

import type { Locale } from "../../types/i18n";
import { useI18nStore } from "../i18n";

const enUS: Locale = {
  id: 1033,
  code: "en-US",
  displayName: "English (United States)",
  englishName: "English (United States)",
  nativeName: "English (United States)",
};
const frCA: Locale = {
  id: 3084,
  code: "fr-CA",
  displayName: "French (Canada)",
  englishName: "French (Canada)",
  nativeName: "français (Canada)",
};

describe("i18nStore", () => {
  beforeEach(() => {
    setActivePinia(createPinia());
  });

  it.concurrent("should be initially empty", () => {
    const i18n = useI18nStore();
    expect(i18n.locale).toBeUndefined();
  });

  it.concurrent("should set the current locale", () => {
    const i18n = useI18nStore();
    i18n.setLocale(enUS);
    expect(i18n.locale?.id).toBe(enUS.id);
    i18n.setLocale(frCA);
    expect(i18n.locale?.id).toBe(frCA.id);
  });
});
