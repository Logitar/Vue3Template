import type { App } from "vue";

import IconButton from "./IconButton.vue";

export default function (app: App) {
  app.component("IconButton", IconButton);
}
