<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

const store = useStore();

const userStore = createNamespacedHelpers(store, "user");
const { user, isLogin } = userStore.useState(["user", "isLogin"]);

const uiStore = createNamespacedHelpers(store, "ui");
const { isOpenSidebar } = uiStore.useState(["isOpenSidebar"]);
const { closeSidebar } = uiStore.useMutations(["closeSidebar"]);
</script>

<template>
	<div class="a-header">
		<div class="a-header__right">
			<p
				v-if="isOpenSidebar"
				class="a-header__icon"
				@click="closeSidebar"
			>
				<i></i>
			</p>

			<h3 class="a-header__heading">
				trang quản trị
				<i class="a-header__heading__icon"></i>
			</h3>
		</div>
		<div class="a-header__left">
			<p class="a-header__icon">
				<i></i>
			</p>
			<div class="a-header__user">
				<img
					src="@/assets/images/default-avatar.jpg"
					alt=""
					class="a-header__user__avatar"
				/>
				<p class="a-header__user__name">
					{{ user.UserName || "Le Huy Hung" }}
					<i class="a-header__user__icon"></i>
				</p>
			</div>
		</div>
	</div>
</template>

<style></style>
