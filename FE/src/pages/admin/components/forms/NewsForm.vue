<script setup>
import Button from "@/components/MButton.vue";
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useRoute, useRouter } from "vue-router";
import { onMounted, computed, reactive } from "vue";
import { useStore } from "vuex";
import * as yup from "yup";
import { Field, useForm, useSubmitForm } from "vee-validate";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const route = useRoute();
const router = useRouter();

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"news"
);

const { currentItem } = useState(["currentItem"]);

const { getNews, insert, update } = useActions(["getNews", "insert", "update"]);

const { clearCurrentItem } = useMutations(["clearCurrentItem"]);

const props = defineProps({
	NewsId: {
		type: String,
		default: "",
	},
});

const state = reactive({
	content: "",
	img: "",
});

onMounted(() => {
	if (currentItem && currentItem.value) {
		setValues(currentItem.value);
		state.content = currentItem.value.Description;
	}
});

const emits = defineEmits(["close", "clear"]);

const title = computed(() => {
	if (props.NewsId) {
		return "Sửa tin tức";
	} else {
		return "Thêm tin tức";
	}
});

const isAdd = computed(() => {
	if (props.NewsId) {
		return false;
	} else {
		return true;
	}
});

const schema = yup.object({
	Title: yup.string(),
	Thumbnail: isAdd
		? yup.mixed().required("Chọn ảnh cho tin tức")
		: yup.string().required(),
	OldThumbail: yup.string(),
	Description: yup.string().required("Nội dung không được để trống"),
});

const { handleSubmit, setValues, setFieldValue } = useForm({
	validationSchema: schema,
	initialValues: currentItem.value || {
		Title: "",
		Thumbnail: "",
		Description: "",
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
			NewsId: "",
			Title: "",
			Thumbnail: "",
			Description: "",
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

const handleChangeText = () => {
	setFieldValue("Description", state.content);

	console.log(state.content);
};

const handleChangeImg = (e) => {
	console.log(e.target.files[0]);

	state.img = URL.createObjectURL(e.target.files[0]);

	setFieldValue("Thumbnail", e.target.files[0]);
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
						<MInput name="Title" label="Tên bài viết" />
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
									@change="handleChangeImg"
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
								<img
									style="width: 100px"
									:src="state.img"
									alt=""
								/>
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
