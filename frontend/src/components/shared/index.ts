import type { App } from "vue";

import AppAlert from "./AppAlert.vue";
import FormInput from "./FormInput.vue";
import IconButton from "./IconButton.vue";
import IconSubmit from "./IconSubmit.vue";

export default function (app: App) {
  app.component("app-alert", AppAlert);
  app.component("form-input", FormInput);
  app.component("icon-button", IconButton);
  app.component("icon-submit", IconSubmit);
}
