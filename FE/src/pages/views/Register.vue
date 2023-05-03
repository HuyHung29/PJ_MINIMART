<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import MInput from "@/components/customs/MInput.vue";
import * as yup from "yup";
import { Field, useForm } from "vee-validate";
import Row from "@/components/bootstrap/Row.vue";
import authApi from "@/api/authAPI";
import { useRouter } from "vue-router";
import { useToast } from "vue-toastification";
import MRadio from "@/components/customs/MRadio.vue";
import RESOURCE from "@/constants/resource";
import MDatePicker from "@/components/customs/MDatePicker.vue";
import { onMounted } from "vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "user");

const { user, isLogin } = useState(["user", "isLogin"]);

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
		UserName: yup.string().required("Tên đăng nhập không được bỏ trống"),
		Password: yup
			.string()
			.min(6, `Mật khẩu cần tối thiếu 6 kí tự`)
			.required("Mật khẩu không được bỏ trống"),
		ConfirmPassword: yup
			.string()
			.oneOf([yup.ref("Password"), null], "Mật khẩu không chính xác"),
		Email: yup
			.string()
			.email("Email không đúng định dạng")
			.required("Email không được để trống"),
		Gender: yup.number(),
		DOB: yup.date(),
		Address: yup.string().required("Địa chỉ không được để trống"),
	})
	.required();

const { handleSubmit } = useForm({
	validationSchema: schema,
	initialValues: {
		FullName: "",
		PhoneNumber: "",
		UserName: "",
		Password: "",
		ConfirmPassword: "",
		Email: "",
		Gender: 0,
		DOB: null,
		Address: "",
	},
});

const onSubmit = handleSubmit(async (values, actions) => {
	try {
		values = { ...values, DOB: new Date(values.DOB) };

		const payload = await authApi.resiger(values);

		toast.success(payload.Message);
		router.push("/login");
	} catch (ex) {
		const { MoreInfo, UserMes, Message } = ex;
		toast.error(UserMes || Message || RESOURCE.HELPTEXT);
		if (MoreInfo) {
			actions.setErrors(MoreInfo);
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
					class="authen__form authen__form--register"
					onSubmit=""
				>
					<h2 class="authen__form__title">Đăng ký</h2>
					<Row>
						<Col md="6" class="has-border">
							<div class="mb-4">
								<MInput name="FullName" label="Họ và tên" />
							</div>
						</Col>
						<Col md="6">
							<div class="mb-4">
								<MInput
									name="PhoneNumber"
									label="Số điện thoại"
								/>
							</div>
						</Col>
						<Col md="6">
							<div class="mb-4">
								<MInput name="Email" label="Email" />
							</div>
						</Col>
						<Col md="6">
							<div class="mb-4">
								<Field
									name="Gender"
									v-slot="{ errorMessage, handleChange }"
								>
									<MRadio
										name="Gender"
										:list="[
											{ title: 'Nam', value: 0 },
											{ title: 'Nữ', value: 1 },
											{ title: 'Khác', value: 2 },
										]"
										:defaultValue="0"
										title="Giới tính"
										@check="handleChange"
										:error="errorMessage"
									/>
								</Field>
							</div>
						</Col>
						<Col md="4">
							<div class="mb-4 authen__password">
								<Field
									name="DOB"
									v-slot="{ handleChange, errorMessage }"
								>
									<MDatePicker
										name="DOB"
										label="Tên đăng nhập"
										@change="handleChange"
										:error="errorMessage"
										title="Ngày sinh"
									/>
								</Field>
							</div>
						</Col>
						<Col md="8">
							<div class="mb-4 authen__password">
								<MInput name="Address" label="Địa chỉ" />
							</div>
						</Col>

						<Col md="12">
							<div class="mb-4 authen__password">
								<MInput name="UserName" label="Tên đăng nhập" />
							</div>
						</Col>

						<Col md="6">
							<div class="mb-4 authen__password">
								<MInput
									name="Password"
									label="Mật khẩu"
									type="password"
								/>
							</div>
						</Col>

						<Col md="6">
							<div
								class="mb-4 authen__password authen__password--confirm"
							>
								<MInput
									name="ConfirmPassword"
									label="Xác nhận mật khẩu"
									type="password"
								/>
							</div>
						</Col>
						<Col md="12" class="d-flex justify-content-center">
							<button
								type="submit"
								class="authen__btn authen__btn--short shadow-none"
							>
								đăng ký
							</button>
						</Col>
					</Row>
					<p class="authen__text">
						Bạn đã có tài khoản?
						<router-link to="/login" class="authen__link">
							Đăng nhập
						</router-link>
					</p>
				</form>
			</Container>
		</div>
	</div>
</template>

<style></style>
