import persistedState from "pinia-plugin-persistedstate";
import { createApp } from "vue";
import { createPinia } from "pinia";

import App from "./App.vue";
import router from "./router";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import "./fontAwesome";

const app = createApp(App);

const pinia = createPinia();
pinia.use(persistedState);

app.use(pinia);
app.use(router);

app.mount("#app");
