import { vue3Debounce } from "vue-debounce";
import { createApp } from "vue";
import "bootstrap/dist/css/bootstrap.css";
import App from "./App.vue";
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import { createRouter, createWebHistory } from "vue-router";
import routes from "@/routes";
import store from "@/store";
import "vue-select/dist/vue-select.css";
import Paginate from "vuejs-paginate-next";
import "@/sass/index.scss";
import { QuillEditor } from "@vueup/vue-quill";
import "@vueup/vue-quill/dist/vue-quill.snow.css";
import VueApexCharts from "vue3-apexcharts";

const router = createRouter({ history: createWebHistory(), routes });

const app = createApp(App);

app.use(Paginate);
app.use(VueApexCharts);
app.directive("debounce", vue3Debounce({ lock: true }));

app.component("QuillEditor", QuillEditor);
app.use(router);

app.use(store);

app.use(Toast, {
	timeout: 2000,
});

app.mount("#app");

import "bootstrap/dist/js/bootstrap";
