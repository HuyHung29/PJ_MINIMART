<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const router = useRouter();

const store = useStore();

const userStore = createNamespacedHelpers(store, "user");
const { user, isLogin } = userStore.useState(["user", "isLogin"]);

const { logout } = userStore.useMutations(["logout"]);

const uiStore = createNamespacedHelpers(store, "ui");
const { isOpenSidebar } = uiStore.useState(["isOpenSidebar"]);
const { closeSidebar } = uiStore.useMutations(["closeSidebar"]);

const handleLogout = () => {
	logout();
	router.replace("/login");
};
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
					{{ user.FullName || "Le Huy Hung" }}
					<i class="a-header__user__icon"></i>
				</p>

				<ul class="header__top__item--sub" v-if="isLogin">
					<li class="header__top__item--sub__item">
						<router-link to="/admin/profile">
							Tài khoản của tôi
						</router-link>
					</li>
					<li class="header__top__item--sub__item">
						<router-link to="/admin/password">
							Mật khẩu
						</router-link>
					</li>
					<li
						class="header__top__item--sub__item"
						@click="handleLogout"
					>
						Đăng xuất
					</li>
				</ul>
			</div>
		</div>
	</div>
</template>

<style scoped>
.a-header {
	z-index: 80;
}
.a-header__user {
	position: relative;
	cursor: pointer;
}

.a-header__user:hover .header__top__item--sub {
	visibility: visible;
	opacity: 1;
	transform: scale(1);
}
</style>
