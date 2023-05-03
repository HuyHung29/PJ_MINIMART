<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import Container from "@/components/bootstrap/Container.vue";
import MInput from "@/components/customs/MInput.vue";
import { useForm } from "vee-validate";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import * as yup from "yup";
import { useToast } from "vue-toastification";
import { onMounted } from "vue";
import RESOURCE from "@/constants/resource";

const store = useStore();
const router = useRouter();

const toast = useToast();

const { useState, useActions } = createNamespacedHelpers(store, "user");

const { user, isLogin } = useState(["user", "isLogin"]);

const { login } = useActions(["login"]);

const schema = yup.object({
	UserName: yup.string().required("Tên đăng nhập không được để trống"),
	Password: yup
		.string()
		.required("Mật khẩu không được để trống")
		.min(6, "Mật khẩu cần ít nhất 6 ký tự"),
});

const { handleSubmit } = useForm({
	validationSchema: schema,
});

const onSubmit = handleSubmit(async (values, actions) => {
	const res = await login({ data: values, callback: actions });

	const { user, isLogin } = store.state.user;

	if (res && isLogin) {
		if (user.Role == 0) {
			router.replace("/admin");
		} else {
			router.replace("/");
		}
	}
});

onMounted(() => {
	if (isLogin.value) {
		if (user.value.Role === RESOURCE.ROLE.ADMIN) {
			router.replace("/admin");
		} else {
			router.replace("/");
		}
	}
});
</script>

<template>
	<div class="authen">
		<div class="authen__header">
			<Container>
				<div
					class="d-flex align-items-md-center justify-content-md-between authen__header--wrap"
				>
					<div class="d-flex align-items-center">
						<router-link to="/">
							<img src="@/assets/images/logo.png" alt="logo" />
						</router-link>
						<h1 class="authen__header__title">Đăng nhập</h1>
					</div>
					<p class="authen__header__text">
						Đăng nhập tài khoản của bạn !
					</p>
				</div>
			</Container>
		</div>
		<div class="authen__content" v-if="!isLogin">
			<Container>
				<form class="authen__form" @submit="onSubmit">
					<h2 class="authen__form__title">Đăng nhập</h2>
					<p v-if="false" class="authen__form__sub-title mb-4">
						Vui lòng xác thực tài khoản trước khi đăng nhập
					</p>
					<div class="mb-5">
						<MInput name="UserName" label="Tên đăng nhập" />
					</div>
					<div class="mb-4 authen__password">
						<MInput
							name="Password"
							label="Mật khẩu"
							type="password"
						/>
					</div>

					<div class="my-4">
						<button
							type="submit"
							class="btn authen__btn shadow-none"
						>
							đăng nhập
						</button>
					</div>

					<p class="authen__text">
						Bạn mới biết đến MiniMart?
						<router-link to="/register" class="authen__link">
							Đăng ký
						</router-link>
					</p>

					<p class="authen__text" onClick="{forgetClick}">
						<span class="authen__link">Quên mật khẩu?</span>
					</p>
				</form>
			</Container>
		</div>
	</div>
</template>

<style></style>
