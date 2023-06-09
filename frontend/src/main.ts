import { createApp } from "vue";
import { createPinia } from "pinia";
import persistedState from "pinia-plugin-persistedstate";

import App from "./App.vue";
import fontAwesome from "./fontAwesome";
import i18n from "./i18n";
import maz from "./maz";
import router from "./router";
import sharedComponents from "./components/shared";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "maz-ui/css/main.css";

import "./assets/styles/main.css";
import "./validation";

const app = createApp(App);

const pinia = createPinia();
pinia.use(persistedState);

app.use(fontAwesome);
app.use(i18n);
app.use(maz);
app.use(pinia);
app.use(router);
app.use(sharedComponents);

app.mount("#app");

// TODO(fpion): always define all emits from components
