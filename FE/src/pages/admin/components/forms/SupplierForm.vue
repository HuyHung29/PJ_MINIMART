<script setup>
import Button from "@/components/MButton.vue";
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useRoute, useRouter } from "vue-router";
import { onMounted, computed } from "vue";
import { useStore } from "vuex";
import * as yup from "yup";
import { useForm, useSubmitForm } from "vee-validate";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const route = useRoute();
const router = useRouter();

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"supplier"
);

const { currentItem } = useState(["currentItem"]);

const { getSupplier, insert, update } = useActions([
	"getSupplier",
	"insert",
	"update",
]);

const { clearCurrentItem } = useMutations(["clearCurrentItem"]);

const props = defineProps({
	SupplierId: {
		type: String,
		default: "",
	},
});

const initData = async (data) => {
	await getSupplier(data);
};

onMounted(() => {
	if (props.SupplierId && props.SupplierId != "") {
		initData(props.SupplierId);
	}
});

const emits = defineEmits(["close", "clear"]);

const title = computed(() => {
	if (props.SupplierId) {
		return "Sửa nhà cung cấp";
	} else {
		return "Thêm nhà cung cấp";
	}
});

const isAdd = computed(() => {
	if (props.SupplierId) {
		return false;
	} else {
		return true;
	}
});

const schema = yup.object({
	SupplierCode: yup.string().required("Mã nhà cung không được để trống"),
	SupplierName: yup.string().required("Tên nhà cung không được để trống"),
	Address: yup.string().required("Địa chỉ nhà cung không được để trống"),
});

const { handleSubmit } = useForm({
	validationSchema: schema,
	initialValues: currentItem || {
		SupplierId: "",
		SupplierName: "",
		SupplierCode: "",
		Address: "",
	},
});

const handleSubmitForm = async (values, action) => {
	let res = false;
	if (isAdd.value) {
		res = await insert({ data: values, callback: action });
	} else {
		res = await update({ data: values, callback: action });
	}

	return res;
};

const onStoreBtnClick = useSubmitForm(async (values, action) => {
	const res = await handleSubmitForm(values, action);

	if (res) {
		clearCurrentItem();
		emits("close");
	}
});

const onStoreAndAddBtnClick = useSubmitForm(async (values, action) => {
	const res = await handleSubmitForm(values, action);

	if (res) {
		action.setValues({
			SupplierId: "",
			SupplierName: "",
			Thumbnail: "",
		});

		action.resetForm();

		clearCurrentItem();
		emits("clear");
	}
});

const handleCloseForm = () => {
	clearCurrentItem();
	emits("close");
};
</script>

<template>
	<div class="overlay">
		<form class="form">
			<div class="form__header">
				<h2 class="form__heading">{{ title }}</h2>

				<div class="form__header__button">
					<p
						class="form__header__button__close"
						@click="handleCloseForm"
					></p>
				</div>
			</div>
			<div class="form__body">
				<Row class="form__row">
					<Col md="6">
						<MInput name="SupplierCode" label="Mã nhà cung cấp" />
					</Col>
					<Col md="6">
						<MInput name="SupplierName" label="Tên nhà cung cấp" />
					</Col>
					<Col md="12">
						<MInput name="Address" label="Địa chỉ" />
					</Col>
				</Row>
			</div>
			<div class="form__action">
				<Button
					type="sub"
					content="Hủy"
					@click="handleCloseForm"
					ref="cancleBtnRef"
				/>
				<div class="form__action__group">
					<Button
						type="sub"
						content="Cất"
						action="submit"
						@click.prevent="onStoreBtnClick"
					/>
					<Button
						content="Cất và thêm"
						action="submit"
						@click="onStoreAndAddBtnClick"
						ref="storeAndAddBtnRef"
					/>
				</div>
			</div>
		</form>
	</div>
</template>
