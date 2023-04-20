import { vue3Debounce } from "vue-debounce";
import { createApp } from "vue";
import "bootstrap/dist/css/bootstrap.css";
import App from "./App.vue";
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import { createRouter, createWebHistory } from "vue-router";
import routes from "@/routes";
import store from "@/store";
import vSelect from "vue-select";
import "vue-select/dist/vue-select.css";

import "@/sass/index.scss";

const router = createRouter({ history: createWebHistory(), routes });

const app = createApp(App);

app.component("v-select", vSelect);
app.directive("debounce", vue3Debounce({ lock: true }));

app.use(router);

app.use(store);

app.use(Toast, {
	timeout: 2000,
});

app.mount("#app");

import "bootstrap/dist/js/bootstrap";
