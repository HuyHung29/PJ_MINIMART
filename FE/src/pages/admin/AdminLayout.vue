<script setup>
import Header from "@/components/admin/AHeader.vue";
import Sidebar from "@/components/admin/ASideBar.vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

const store = useStore();

const uiStore = createNamespacedHelpers(store, "ui");
const { isOpenSidebar } = uiStore.useState(["isOpenSidebar"]);
</script>

<template>
	<div
		class="a-container"
		:class="{ 'a-container--sidebar-close': !isOpenSidebar }"
	>
		<Sidebar />

		<div class="a-content">
			<Header />

			<div class="a-body">
				<router-view />
			</div>
		</div>
	</div>
</template>

<style scoped>
* {
	box-sizing: border-box;
	color: #212121;
	font-size: 14px;
	line-height: 18px;
}

h1 {
	font-weight: 600;
	font-size: 20px;
	line-height: 26px;
}

h2 {
	font-weight: 600;
	font-size: 18px;
	line-height: 20px;
}

h3 {
	font-weight: 600;
	font-size: 16px;
	line-height: 20px;
}

.wrapper {
	background-color: var(--background-color);
	padding: 24px;

	display: flex;
	flex-direction: column;
	justify-content: flex-start;
	align-items: flex-start;
	row-gap: 16px;

	height: 100vh;
}

.text-center {
	text-align: center;
}

.text-right {
	text-align: right;
}
</style>
