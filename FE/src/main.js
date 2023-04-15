import { createApp } from "vue";
import "bootstrap/dist/css/bootstrap.css";
import App from "./App.vue";
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import { createRouter, createWebHistory } from "vue-router";
import routes from "@/routes";
import store from "@/store";

import "@/sass/index.scss";

const router = createRouter({ history: createWebHistory(), routes });

const app = createApp(App);

app.use(router);

app.use(store);

app.use(Toast);

app.mount("#app");

import "bootstrap/dist/js/bootstrap";
