<script setup>
import MInput from "@/components/customs/MInput.vue";
import { useForm } from "vee-validate";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import * as yup from "yup";

const store = useStore();
const router = useRouter();

const { useState, useActions } = createNamespacedHelpers(store, "user");

const { user } = useState(["user"]);

const { changePassword } = useActions(["changePassword"]);

const schema = yup
	.object({
		Password: yup
			.string()
			.min(6, `Mật khẩu cần tối thiếu 6 kí tự`)
			.required("Mật khẩu không được bỏ trống"),
		NewPassword: yup
			.string()
			.min(6, `Mật khẩu cần tối thiếu 6 kí tự`)
			.required("Mật khẩu không được bỏ trống"),
		ConfirmPassword: yup
			.string()
			.oneOf([yup.ref("NewPassword"), null], "Mật khẩu không chính xác"),
	})
	.required();

const { handleSubmit, setValues } = useForm({
	validationSchema: schema,
	initialValues: {
		Password: "",
		NewPassword: "",
	},
});

const onSubmit = handleSubmit(async (values, actions) => {
	console.log(values);

	const res = await changePassword(values);

	if (res) {
		actions.resetForm();
	}
});
</script>

<template>
	<div class="profile">
		<div class="profile__header">
			<h2 class="profile__heading">Đổi mật khẩu</h2>
			<p class="profile__sub-heading">
				Để bảo mật tài khoản, vui lòng không chia sẻ mật khẩu cho người
				khác
			</p>
		</div>
		<form class="profile__user d-flex" id="profile-form" @submit="onSubmit">
			<div class="profile__user__contain">
				<div class="profile__user__field">
					<p class="profile__user__label xl">Mật khẩu hiện tại</p>
					<MInput name="Password" type="password" />
				</div>
				<div class="profile__user__field">
					<p class="profile__user__label xl">Mật khẩu mới</p>
					<MInput name="NewPassword" type="password" />
				</div>
				<div class="profile__user__field">
					<p class="profile__user__label xl">Xác nhận mật khẩu</p>

					<MInput name="ConfirmPassword" type="password" />
				</div>

				<button class="btn profile__user__action xl" type="submit">
					Xác nhận
				</button>
			</div>
		</form>
	</div>
</template>

<style scoped>
.btn.profile__user__action {
	color: white;
}
</style>
