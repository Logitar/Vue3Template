import JsonViewer from "vue3-json-viewer";
import type { App } from "vue";

import "vue3-json-viewer/dist/index.css";

export default function (app: App) {
  app.use(JsonViewer);
}
