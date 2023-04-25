<script setup>
import MInput from "@/components/customs/MInput.vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import * as yup from "yup";
import { useRouter } from "vue-router";
import { Field, useForm } from "vee-validate";
import { onMounted, ref } from "vue";
import MRadio from "@/components/customs/MRadio.vue";
import Row from "@/components/bootstrap/Row.vue";
import Col from "@/components/bootstrap/Col.vue";
import MDatePicker from "@/components/customs/MDatePicker.vue";
import MButton from "@/components/MButton.vue";

const store = useStore();
const router = useRouter();

const { useState, useActions } = createNamespacedHelpers(store, "user");

const { user } = useState(["user"]);

const { edit } = useActions(["edit"]);

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
		Gender: yup.number(),
		DOB: yup.date().required("Ngày sinh không được để trống"),
		Address: yup.string().required("Địa chỉ không được để trống"),
	})
	.required();

onMounted(() => {
	setValues(user.value);
});

const isView = ref(true);

const { handleSubmit, setValues } = useForm({
	validationSchema: schema,
	initialValues: { ...user.value },
});

const onSubmit = handleSubmit(async (values, actions) => {
	values = { ...values, DOB: new Date(values.DOB) };
	const res = await edit({ data: values, callback: actions });

	if (res) {
		isView.value = true;
	}
});
</script>

<template>
	<div class="profile">
		<div class="profile__header">
			<h2 class="profile__heading">Hồ sơ của tôi</h2>
			<p class="profile__sub-heading mt-4">
				Quản lý thông tin hồ sơ để bảo mật tài khoản
			</p>
		</div>
		<form class="profile__user" id="profile-form" @submit="onSubmit">
			<div class="profile__user__field">
				<p class="profile__user__label">Tên đăng nhập</p>
				<p class="profile__user__data">{{ user.UserName }}</p>
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Email</p>
				<MInput name="Email" :readonly="true" />
			</div>
			<Row>
				<Col md="6">
					<div class="profile__user__field">
						<p class="profile__user__label">Họ tên</p>
						<MInput :readonly="isView" name="FullName" />
					</div>
				</Col>
				<Col md="6">
					<div class="profile__user__field">
						<p class="profile__user__label">Số điện thoại</p>
						<MInput :readonly="isView" name="PhoneNumber" />
					</div>
				</Col>
				<Col md="6">
					<div class="profile__user__field">
						<p class="profile__user__label">Giới tính</p>
						<Field
							name="Gender"
							v-slot="{ handleChange, value, errorMessage }"
						>
							<MRadio
								name="Gender"
								:list="[
									{ title: 'Nam', value: 0 },
									{ title: 'Nữ', value: 1 },
									{ title: 'Khác', value: 2 },
								]"
								:defaultValue="value"
								@check="handleChange"
								:error="errorMessage"
							/>
						</Field>
					</div>
				</Col>
				<Col md="6">
					<div class="profile__user__field">
						<p class="profile__user__label">Ngày sinh</p>
						<Field
							name="DOB"
							v-slot="{ handleChange, errorMessage, value }"
						>
							<MDatePicker
								name="DOB"
								label="Tên đăng nhập"
								@change="handleChange"
								:error="errorMessage"
								:value="value"
								:readonly="isView"
							/>
						</Field>
					</div>
				</Col>
				<Col md="12">
					<div class="profile__user__field address">
						<p class="profile__user__label">Địa chỉ</p>
						<MInput :readonly="isView" name="Address" />
					</div>
				</Col>
			</Row>
			<button
				v-if="!isView"
				class="btn profile__user__action"
				type="submit"
			>
				Cập nhật
			</button>
			<MButton
				v-if="isView"
				@click="isView = false"
				content="Thay đổi thông tin"
			/>
		</form>
	</div>
</template>

<style>
.profile__user__action {
	color: white;
}

.radiofield.radiofield--sm {
	display: flex;
	align-items: center;
}

.date-picker {
	min-width: 260px;
}

.address {
	margin-right: 40px;
}

.address .textfield {
	width: 100%;
}

.c-btn {
	margin: 0 auto;
}
</style>
