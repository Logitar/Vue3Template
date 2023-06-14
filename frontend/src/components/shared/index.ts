import type { App } from "vue";

import AppAlert from "./AppAlert.vue";
import AppAvatar from "./AppAvatar.vue";
import AppBadge from "./AppBadge.vue";
import AppPagination from "./AppPagination.vue";
import AppTab from "./AppTab.vue";
import AppTabs from "./AppTabs.vue";
import CountSelect from "./CountSelect.vue";
import DateTimeInput from "./DateTimeInput.vue";
import FormCheckbox from "./FormCheckbox.vue";
import FormInput from "./FormInput.vue";
import FormSelect from "./FormSelect.vue";
import IconButton from "./IconButton.vue";
import IconSubmit from "./IconSubmit.vue";
import SearchInput from "./SearchInput.vue";
import SortSelect from "./SortSelect.vue";
import StatusBlock from "./StatusBlock.vue";
import UrlInput from "./UrlInput.vue";

export default function (app: App) {
  app.component("app-alert", AppAlert);
  app.component("app-avatar", AppAvatar);
  app.component("app-badge", AppBadge);
  app.component("app-pagination", AppPagination);
  app.component("app-tab", AppTab);
  app.component("app-tabs", AppTabs);
  app.component("count-select", CountSelect);
  app.component("date-time-input", DateTimeInput);
  app.component("form-checkbox", FormCheckbox);
  app.component("form-input", FormInput);
  app.component("form-select", FormSelect);
  app.component("icon-button", IconButton);
  app.component("icon-submit", IconSubmit);
  app.component("search-input", SearchInput);
  app.component("sort-select", SortSelect);
  app.component("status-block", StatusBlock);
  app.component("url-input", UrlInput);
}
