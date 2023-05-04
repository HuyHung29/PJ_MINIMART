<script setup>
import Button from "@/components/MButton.vue";
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useRoute, useRouter } from "vue-router";
import { onMounted, computed, ref, reactive, watch } from "vue";
import { useStore } from "vuex";
import * as yup from "yup";
import { Field, useForm, useSubmitForm } from "vee-validate";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import MSelect from "@/components/customs/MSelect.vue";
import RESOURCES from "@/constants/resource";

const route = useRoute();
const router = useRouter();

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"product"
);

const suppStore = createNamespacedHelpers(store, "supplier");

const { supplier } = suppStore.useState(["supplier"]);

const cateStore = createNamespacedHelpers(store, "category");
const { category } = cateStore.useState(["category"]);

const { currentItem } = useState(["currentItem"]);

const { getProduct, insert, update } = useActions([
	"getProduct",
	"insert",
	"update",
]);

const { clearCurrentItem } = useMutations(["clearCurrentItem"]);

const props = defineProps({
	ProductId: {
		type: String,
		default: "",
	},
});

// const initData = async (data) => {
// 	await getProduct(data);
// };

const state = reactive({
	cateOptions: [],
	suppOptions: [],
	content: "",
	imgs: [],
});

onMounted(() => {
	state.cateOptions = category.value.map((item) => ({
		value: item.CategoryId,
		title: item.CategoryName,
	}));

	state.suppOptions = supplier.value.map((item) => ({
		value: item.SupplierId,
		title: item.SupplierName,
	}));

	if (currentItem && currentItem.value) {
		setValues(currentItem.value);
		state.content = currentItem.value.Description;
	} else {
		setValues({
			ProductName: "",
			ProductCode: "",
			Quantity: 0,
			Weight: 0,
			Price: 0,
			Discount: 0,
			SupplierId: "",
			CategoryId: "",
			Unit: 0,
			Volume: 0,
			Description: "",
			Pictures: "",
		});
	}
});

const emits = defineEmits(["close", "clear"]);

const title = computed(() => {
	if (props.ProductId) {
		return "Sửa sản phẩm";
	} else {
		return "Thêm sản phẩm";
	}
});

const isAdd = computed(() => {
	if (props.ProductId) {
		return false;
	} else {
		return true;
	}
});

const schema = yup.object({
	ProductCode: yup.string().required("Mã sản phẩm không được để trống"),
	ProductName: yup.string().required("Tên sản phẩm không được để trống"),
	Quantity: yup
		.number("Số lượng phải là số")
		.min(0, "Số lượng phải lớn hơn 0")
		.required("Số lượng không được bỏ trống"),
	Weight: yup
		.number("Dung tích/Khối lượng phải là số")
		.min(0, "Dung tích/Khối lượng phải lớn hơn 0")
		.required("Dung tích/Khối lượng không được để trống"),
	Price: yup
		.number("Giá phải là số")
		.min(0, "Giá phải lớn hơn 0")
		.required("Giá không được để trống"),
	Discount: yup.string("Giá phải là số").min(0, "Giá phải lớn hơn 0"),
	SupplierId: yup.string().required("Nhà cung cấp không được để trống"),
	CategoryId: yup.string().required("Danh mục không được để trống"),
	Unit: yup.number(),
	Volume: yup.number(),
	Description: yup.string().required("Mô tả không được để trống"),
	Pictures: isAdd
		? yup.mixed().required("Chọn ảnh cho danh mục")
		: yup.mixed().required("Chọn ảnh cho danh mục"),
});

const { handleSubmit, setFieldValue, setValues } = useForm({
	validationSchema: schema,
	initialValues: currentItem || {
		ProductId: "",
		ProductName: "",
		ProductCode: "",
		Quantity: 0,
		Weight: 0,
		Price: 0,
		Discount: 0,
		SupplierId: "",
		CategoryId: "",
		Unit: 0,
		Volume: 0,
		Description: "",
		Pictures: "",
		MainQuantity: 0,
	},
});

const handleSubmitForm = async (values, action) => {
	console.log(values);
	let res = false;
	if (isAdd.value) {
		res = await insert({ data: values, callback: action });
	} else {
		res = await update({ data: values, callback: action });
	}

	return res;
};

const onStoreBtnClick = useSubmitForm(async (values, action) => {
	console.log(values);
	const res = await handleSubmitForm(values, action);
	if (res) {
		clearCurrentItem();
		state.content = "";
		emits("close");
	}
});

// const content = ref(currentItem?.Description || "");

const onStoreAndAddBtnClick = useSubmitForm(async (values, action) => {
	const res = await handleSubmitForm(values, action);
	if (res) {
		action.setValues({
			ProductId: "",
			SupplierName: "",
			Thumbnail: "",
		});

		action.resetForm();

		clearCurrentItem();

		state.content = "";
		emits("clear");
	}
});

const handleCloseForm = () => {
	clearCurrentItem();
	emits("close");
};

const handleChangeText = () => {
	setFieldValue("Description", state.content);

	console.log(state.content);
};

const handleChangeImg = (e) => {
	const files = e.target.files;

	state.imgs = [...files].map((file) => {
		return URL.createObjectURL(file);
	});

	setFieldValue("Pictures", files);
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
						<MInput name="ProductCode" label="Mã sản phẩm" />
					</Col>
					<Col md="6">
						<MInput name="ProductName" label="Tên sản phẩm" />
					</Col>
					<Col md="6">
						<MInput name="Quantity" label="Số lượng" />
					</Col>
					<Col md="6">
						<Field v-slot="{ handleChange, value }" name="Unit">
							<MSelect
								title="Đơn vị tính"
								:options="RESOURCES.UNIT"
								:defaultValue="
									RESOURCES.UNIT.find((unit) =>
										value
											? unit.value == value
											: unit.value == 0
									).title
								"
								@select="handleChange"
								:initialValue="RESOURCES.UNIT[0].value"
							/>
						</Field>
					</Col>
					<Col md="6">
						<Field
							v-slot="{ handleChange, value }"
							name="SupplierId"
						>
							<MSelect
								title="Nhà cung cấp"
								:options="state.suppOptions"
								:defaultValue="
									state.suppOptions?.find((supp) =>
										value
											? supp.value == value
											: supp.value ==
											  state.suppOptions[0].value
									)?.title
								"
								@select="handleChange"
								:initialValue="
									currentItem?.SupplierId ||
									state?.suppOptions[0]?.value
								"
							/>
						</Field>
					</Col>
					<Col md="6">
						<Field
							v-slot="{ handleChange, value, errorMessage }"
							name="CategoryId"
						>
							<MSelect
								title="Danh mục"
								:options="state.cateOptions"
								:defaultValue="
									state?.cateOptions?.find((cate) =>
										value
											? cate.value == value
											: cate.value ==
											  state.cateOptions[0].value
									)?.title
								"
								@select="handleChange"
								:initialValue="
									currentItem?.CategoryId ||
									state?.cateOptions[0]?.value
								"
								:error="errorMessage"
							/>
						</Field>
					</Col>
					<Col md="6">
						<MInput name="Weight" label="Dung tích/Khối lượng" />
					</Col>
					<Col md="6">
						<Field
							v-slot="{ handleChange, value, errorMessage }"
							name="Volume"
						>
							<MSelect
								title="Khối lượng"
								:options="RESOURCES.VOLUME"
								:defaultValue="
									RESOURCES.VOLUME.find((volume) =>
										value
											? volume.value == value
											: volume.value == 0
									).title
								"
								:initialValue="RESOURCES.VOLUME[0].value"
								@select="handleChange"
								:error="errorMessage"
							/>
						</Field>
					</Col>
					<Col md="6">
						<MInput name="Price" label="Giá" />
					</Col>
					<Col md="6">
						<MInput name="Discount" label="Giảm giá" />
					</Col>
					<Col md="12">
						<Field
							name="Pictures"
							v-slot="{ handleChange, handleBlur }"
						>
							<div class="input-group mb-3">
								<label
									class="input-group-text"
									for="inputGroupFile01"
									>Chọn ảnh</label
								>
								<input
									hidden
									multiple
									accept="image/*"
									@change="handleChangeImg"
									@blur="handleBlur"
									type="file"
									class="form-control"
									id="inputGroupFile01"
								/>
								<div class="invalid-feedback">
									{{ "Ảnh sản phẩm không đưuọc để trống" }}
								</div>
							</div>
							<div class="img-preview">
								<img
									v-for="(item, index) in state.imgs"
									:key="index"
									:src="item"
									alt=""
								/>
							</div>
						</Field>
						<img
							v-if="currentItem"
							v-for="(item, index) in currentItem.OldPictures"
							:key="index"
							style="width: 100px"
							:src="item.Url"
							alt=""
						/>
					</Col>
					<Col md="12">
						<Field
							name="Description"
							v-slot="{ value, errors, errorMessage }"
						>
							<QuillEditor
								theme="snow"
								v-model:content="state.content"
								@textChange="handleChangeText"
								@ready=""
								content-type="html"
								:content="currentItem.Description || value"
							/>
							<div :class="{ 'is-invalid': errors }">
								<div class="invalid-feedback">
									{{ errorMessage }}
								</div>
							</div>
						</Field>
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

<style scoped>
.form {
	position: relative;
	min-width: 1200px;
	max-width: 1200px;
	background-color: white;
	border-radius: 4px;
	min-height: 680px;
	max-height: 680px;
}

.form__body {
	padding: 24px;
	min-height: 600px !important;
	overflow-y: auto;
	overflow-x: hidden;
	max-height: 500px;
}

.img-preview {
	display: flex;
	align-items: center;
	column-gap: 10px;
}

.img-preview img {
	width: 60px;

	border: 1px solid #ccc;
}
</style>
