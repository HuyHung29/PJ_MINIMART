<script setup>
import Loading from "@/components/MLoading.vue";
import Dialog from "@/components/Dialog.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const store = useStore();

const uiStore = createNamespacedHelpers(store, "ui");
const { isLoading, modal } = uiStore.useState(["isLoading", "modal"]);
</script>

<template>
	<router-view></router-view>
	<Loading v-if="isLoading" />
	<Dialog v-if="modal.isOpen" />
</template>

<style></style>
