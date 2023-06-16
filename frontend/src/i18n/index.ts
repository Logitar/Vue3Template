import { createI18n } from "vue-i18n";

import en from "./en";
import fr from "./fr";

type MessageSchema = typeof en;

export default createI18n<[MessageSchema], "en" | "fr">({
  legacy: false,
  locale: "en",
  fallbackLocale: "en",
  messages: {
    en,
    fr,
  },
  datetimeFormats: {
    en: {
      medium: {
        year: "numeric",
        month: "short",
        day: "numeric",
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
      },
    },
    fr: {
      medium: {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
      },
    },
  },
});

// TODO(fpion): "npm run type-check" won't run
