<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import MInput from "@/components/customs/MInput.vue";
import * as yup from "yup";
import { useForm } from "vee-validate";
import Row from "@/components/bootstrap/Row.vue";
import authApi from "@/api/authAPI";
import { useRouter } from "vue-router";
import { useToast } from "vue-toastification";

const router = useRouter();
const toast = useToast();

const schema = yup
	.object({
		FullName: yup
			.string()
			.min(6, `Cần tối thiếu 6 kí tự`)
			.required("Họ tên không được để trống"),
		PhoneNumber: yup.string().matches(/^[0-9]{9,}$/, {
			message: "Số điện thoại phải có nhiều hơn 9 chữ số",
			excludeEmptyString: true,
		}),
		UserName: yup
			.string()
			.email("Email không đúng định dạng")
			.required("Email không được bỏ trống"),
		Password: yup
			.string()
			.min(6, `Cần tối thiếu 6 kí tự`)
			.required("Mật khẩu không được bỏ trống"),
		ConfirmPassword: yup
			.string()
			.oneOf([yup.ref("Password"), null], "Mật khẩu không chính xác"),
	})
	.required();

const { handleSubmit } = useForm({
	validationSchema: schema,
});

const onSubmit = handleSubmit(async (values, actions) => {
	try {
		console.log(values);
		const payload = await authApi.resiger(values);

		toast.success(payload.Message);
		router.push("/login");
	} catch (ex) {
		const { MoreInfo, UserMes, Message } = ex;
		toast.error(UserMes || Message);
		actions.setErrors(MoreInfo);
	}
});
</script>

<template>
	<div class="authen">
		<div class="authen__header">
			<Container>
				<div
					class="d-flex align-items-center justify-content-between authen__header--wrap"
				>
					<div class="d-flex align-items-center">
						<router-link to="/">
							<img src="@/assets/images/logo.png" alt="logo" />
						</router-link>
						<h1 class="authen__header__title">Đăng ký</h1>
					</div>
					<p class="authen__header__text">Đăng ký tài khoản mới !</p>
				</div>
			</Container>
		</div>
		<div class="authen__content">
			<Container>
				<form
					@submit="onSubmit"
					className="authen__form authen__form--register"
					onSubmit=""
				>
					<h2 className="authen__form__title">Đăng ký</h2>
					<Row>
						<div class="mb-4">
							<MInput name="FullName" label="Họ và tên" />
						</div>
						<div class="mb-4">
							<MInput name="PhoneNumber" label="Số điện thoại" />
						</div>
						<div class="mb-4">
							<MInput name="UserName" label="Email" />
						</div>
						<div class="mb-4 authen__password">
							<MInput
								name="Password"
								label="Mật khẩu"
								type="password"
							/>
						</div>
						<div
							class="mb-4 authen__password authen__password--confirm"
						>
							<MInput
								name="ConfirmPassword"
								label="Xác nhận mật khẩu"
								type="password"
							/>
						</div>
						<Col md="12" className="d-flex justify-content-center">
							<button
								type="submit"
								className="authen__btn authen__btn--short shadow-none"
							>
								đăng ký
							</button>
						</Col>
					</Row>
					<p className="authen__text">
						Bạn đã có tài khoản?
						<router-link to="/login" className="authen__link">
							Đăng nhập
						</router-link>
					</p>
				</form>
			</Container>
		</div>
	</div>
</template>

<style></style>
