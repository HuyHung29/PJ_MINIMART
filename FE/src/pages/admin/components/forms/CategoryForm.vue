<script setup>
import Button from "@/components/MButton.vue";
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useRoute, useRouter } from "vue-router";
import { onMounted, computed } from "vue";
import { useStore } from "vuex";
import * as yup from "yup";
import { Field, useForm, useSubmitForm } from "vee-validate";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const route = useRoute();
const router = useRouter();

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"category"
);

const { currentItem } = useState(["currentItem"]);

const { getCategory, insert, update } = useActions([
	"getCategory",
	"insert",
	"update",
]);

const { clearCurrentItem } = useMutations(["clearCurrentItem"]);

const props = defineProps({
	CategoryId: {
		type: String,
		default: "",
	},
});

const initData = async (data) => {
	await getCategory(data);
};

onMounted(() => {
	if (props.CategoryId && props.CategoryId != "") {
		initData(props.CategoryId);
	}
});

const emits = defineEmits(["close", "clear"]);

const title = computed(() => {
	if (props.CategoryId) {
		return "Sửa danh mục";
	} else {
		return "Thêm danh mục";
	}
});

const isAdd = computed(() => {
	if (props.CategoryId) {
		return false;
	} else {
		return true;
	}
});

const schema = yup.object({
	CategoryName: yup.string().required("Tên danh mục không được để trống"),
	Thumbnail: isAdd
		? yup.mixed().required("Chọn ảnh cho danh mục")
		: yup.string().required(),
	OldThumbail: yup.string(),
});

const { handleSubmit } = useForm({
	validationSchema: schema,
	initialValues: currentItem || {
		CategoryId: "",
		CategoryName: "",
		Thumbnail: "",
	},
});

const handleSubmitForm = async (values, action) => {
	if (isAdd.value) {
		await insert({ data: values, callback: action });
	} else {
		await update({ data: values, callback: action });
	}
};

const onStoreBtnClick = useSubmitForm(async (values, action) => {
	await handleSubmitForm(values, action);

	clearCurrentItem();
	emits("close");
});

const onStoreAndAddBtnClick = useSubmitForm(async (values, action) => {
	await handleSubmitForm(values, action);

	action.setValues({
		CategoryId: "",
		CategoryName: "",
		Thumbnail: "",
	});

	action.resetForm();

	clearCurrentItem();
	emits("clear");
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
					<Col md="12">
						<MInput name="CategoryName" label="Tên danh mục" />
					</Col>
					<Col md="12">
						<Field
							name="Thumbnail"
							v-slot="{
								handleChange,
								handleBlur,
								errorMessage,
								errors,
							}"
						>
							<div class="input-group mb-3">
								<label
									class="input-group-text"
									for="inputGroupFile01"
									>Chọn ảnh</label
								>
								<input
									hidden
									accept="image/*"
									@change="handleChange"
									@blur="handleBlur"
									type="file"
									class="form-control"
									:class="{
										'is-invalid': errors,
									}"
									id="inputGroupFile01"
								/>
								<div class="invalid-feedback">
									{{ errorMessage }}
								</div>
							</div>
						</Field>
						<img
							v-if="currentItem.OldThumbnail"
							style="width: 100px"
							:src="currentItem.OldThumbnail"
							alt=""
						/>
						<Field name="OldThumbnail">
							<input type="text" hidden />
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
