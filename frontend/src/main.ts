import { createApp } from "vue";
import { createPinia } from "pinia";

import App from "./App.vue";
import fontAwesome from "./fontAwesome";
import i18n from "./i18n";
import router from "./router";
import sharedComponents from "./components/shared";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import "./assets/main.css";
import "./validation";

const app = createApp(App);

app.use(createPinia());
app.use(fontAwesome);
app.use(i18n);
app.use(router);
app.use(sharedComponents);

app.mount("#app");
