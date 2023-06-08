import type { App } from "vue";

import AppAlert from "./AppAlert.vue";
import AppBadge from "./AppBadge.vue";
import AppTabs from "./AppTabs.vue";
import DateTimeInput from "./DateTimeInput.vue";
import FormCheckbox from "./FormCheckbox.vue";
import FormInput from "./FormInput.vue";
import FormSelect from "./FormSelect.vue";
import IconButton from "./IconButton.vue";
import IconSubmit from "./IconSubmit.vue";
import TabContents from "./TabContents.vue";
import TabHeader from "./TabHeader.vue";
import UrlInput from "./UrlInput.vue";

export default function (app: App) {
  app.component("app-alert", AppAlert);
  app.component("app-badge", AppBadge);
  app.component("app-tabs", AppTabs);
  app.component("date-time-input", DateTimeInput);
  app.component("form-checkbox", FormCheckbox);
  app.component("form-input", FormInput);
  app.component("form-select", FormSelect);
  app.component("icon-button", IconButton);
  app.component("icon-submit", IconSubmit);
  app.component("tab-contents", TabContents);
  app.component("tab-header", TabHeader);
  app.component("url-input", UrlInput);
}
