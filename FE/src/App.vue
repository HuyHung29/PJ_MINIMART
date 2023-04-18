<script setup>
import Loading from "@/components/MLoading.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const store = useStore();

const uiStore = createNamespacedHelpers(store, "ui");
const { isLoading } = uiStore.useState(["isLoading"]);
</script>

<template>
	<router-view></router-view>
	<Loading v-if="isLoading" />
</template>

<style></style>
