<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useForm } from "vee-validate";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import * as yup from "yup";

const emits = defineEmits(["close"]);

const handleClose = () => {
	emits("close");
};

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "address");

const { insert } = useActions(["insert"]);

const schema = yup
	.object({
		Receiver: yup.string().required("Người nhận không được để trống"),
		PhoneNumber: yup.string().required("Số điện thoại không được để trống"),
		City: yup.string().required("Thành phố không được để trống"),
		District: yup.string().required("Quận/Huyện không được để trống"),
		Ward: yup.string().required("Xã/Phường không được để trống"),
		Detail: yup.string().required("Số nhà không được để trống"),
	})
	.required();

const { handleSubmit, setFieldValue } = useForm({
	validationSchema: schema,
	initialValues: {
		Receiver: "",
		PhoneNumber: "",
		City: "",
		District: "",
		Ward: "",
		Detail: "",
	},
});

const onSubmit = handleSubmit(async (values, actions) => {
	console.log(values);

	const res = await insert({ data: values, callback: actions });

	if (res) {
		actions.resetForm();
		emits("close");
	}
});
</script>

<template>
	<div class="profile__address active">
		<form class="profile__address__form" @submit="onSubmit">
			<Row>
				<h2>Thêm địa chỉ</h2>

				<Col md="6">
					<MInput name="Receiver" label="Người nhận" />
				</Col>

				<Col md="6">
					<MInput name="PhoneNumber" label="Số điện thoại" />
				</Col>

				<Col md="6">
					<MInput name="City" label="Thành phố" />
				</Col>

				<Col md="6">
					<MInput name="District" label="Quận/Huyện" />
				</Col>

				<Col md="6">
					<MInput name="Ward" label="Xã/Phường" />
				</Col>

				<Col md="6">
					<MInput name="Detail" label="Số nhà" />
				</Col>

				<Col md="12" class="profile__address__form__action">
					<button
						class="btn btn-no-border"
						type="reset"
						@click="handleClose"
					>
						Trở lại
					</button>
					<button class="btn btn-normal" type="submit">
						Hoàn thành
					</button>
				</Col>
			</Row>
		</form>
	</div>
</template>

<style scoped>
.row {
	row-gap: 20px;
}
</style>
