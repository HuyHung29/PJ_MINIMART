<script setup>
import MInput from "@/components/customs/MInput.vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"user"
);

const { user, isLogin } = useState(["user", "isLogin"]);

const { logout } = useMutations(["logout"]);

const handleLogout = () => {
	logout();
	router.replace("/login");
};
</script>

<template>
	<div class="profile">
		<div class="profile__header">
			<h2 class="profile__heading">Hồ sơ của tôi</h2>
			<p class="profile__sub-heading mt-4">
				Quản lý thông tin hồ sơ để bảo mật tài khoản
			</p>
		</div>
		<form
			class="profile__user"
			id="profile-form"
			onSubmit="{handleSubmit(onUserChangeInfo)}"
		>
			<div class="profile__user__field">
				<p class="profile__user__label">Tên đăng nhập</p>
				<p class="profile__user__data">huyhung29</p>
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Họ tên</p>
				<MInput name="FullName" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Giới tính</p>
				<MInput name="FullName" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Số điện thoại</p>
				<MInput name="PhoneNumber" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Email</p>
				<MInput name="Email" :readonly="true" />
			</div>
			<button class="btn profile__user__action" type="submit">Lưu</button>
		</form>
	</div>
</template>

<style>
.profile__user__action {
	color: white;
}
</style>
