<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import CategoryForm from "./components/forms/CategoryForm.vue";
import CategoryItem from "./components/items/CategoryItem.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchCategory({ ...value });
	}
);

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"category"
);

const { category, pagination } = useState(["category", "pagination"]);

const { fetchCategory, remove } = useActions(["fetchCategory", "remove"]);

const { listCate = category } = useGetters(["category"]);

const uiStore = createNamespacedHelpers(store, "ui");

const { modal } = uiStore.useState(["modal"]);

const { handleOpenModal } = uiStore.useMutations(["handleOpenModal"]);
/**
 * Định nghĩa các state
 * Author: LHH - 04/01/23
 */
const state = reactive({
	checkList: [],
	isShowForm: false,
	CategoryId: "",
});

/**
 * Hàm xử lý check all
 * Author: LHH - 14/02/23
 */
const isCheckAll = computed(() => {
	let isCheck = true;

	if (state.checkList.length === 0) {
		return false;
	}

	isCheck = category._value.every((item) =>
		state.checkList.includes(item.CategoryId)
	);

	return isCheck;
});

const initData = async () => {
	await fetchCategory();
};

/**
 * Call API
 * Author: LHH - 04/01/23
 */
onBeforeMount(() => {
	initData();
});

const handleUpdate = (value) => {
	console.log(value);
	state.isShowForm = true;
	state.CategoryId = value.CategoryId;
};

const handleDelete = (value) => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [value.CategoryId],
		});
	} catch (error) {
		console.log(error);
	}
};

const handleMultipleDelete = () => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING_MULTIPLE,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [...state.checkList],
		});

		state.checkList = [];
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý check all
 * Author: LHH - 04/01/23
 */
const handleCheckAll = (target) => {
	try {
		const cateIds = listCate._value.map((item) => item.CategoryId);

		const ids = cateIds.filter((id) => !state.checkList.includes(id));
		if (target.checked) {
			state.checkList = [...state.checkList, ...ids];
		} else {
			state.checkList = [
				...state.checkList.filter((item) => !cateIds.includes(item)),
			];
		}
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý check item
 * Author: LHH - 04/01/23
 */
const handleCheck = (value) => {
	try {
		console.log(value);
		if (state.checkList.includes(value)) {
			state.checkList.splice(state.checkList.indexOf(value), 1);
		} else {
			state.checkList.push(value);
		}
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý tìm kiếm
 * Author: LHH - 04/01/23
 */
const handleSearchEmployee = (value) => {
	try {
		if (value) {
		} else {
		}
	} catch (error) {
		console.log(error);
	}
};

const handleCloseForm = () => {
	state.isShowForm = false;
	state.CategoryId = "";
};

const handleClearForm = () => {
	state.CategoryId = "";
};
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Danh mục</h2>
			<Button
				content="Thêm mới danh mục"
				@click="state.isShowForm = true"
			/>
		</div>

		<div class="c-table-wrapper">
			<div class="c-table__function">
				<div
					class="c-table__function_multiple-task"
					v-show="state.checkList.length >= 1"
				>
					<p class="c-table__function_multiple-task__text">
						Đã chọn {{ state.checkList.length }}
					</p>
					<p
						class="c-table__function_multiple-task__text warning"
						@click="state.checkList = []"
					>
						Bỏ chọn
					</p>

					<button
						class="c-table__function_multiple-task__delete"
						@click="handleMultipleDelete"
						:disabled="state.checkList.length < 1"
					>
						<p class="icon">
							<i></i>
						</p>
						Xóa
					</button>
				</div>
				<div class="textfield textfield--sm">
					<div class="textfield__input__wrap">
						<p class="textfield__icon">
							<i></i>
						</p>
						<input
							type="text"
							class="textfield__input"
							placeholder="Tìm theo mã, tên nhân viên"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearchEmployee"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchCategory">
					<i></i>
				</p>
				<p class="c-table__function__export" @click="handleExportData">
					<i></i>
				</p>
			</div>

			<div class="c-table__wrap">
				<table class="c-table">
					<thead class="c-table__header">
						<tr class="c-table__row">
							<th class="c-table__heading text-center">
								<CheckBox
									id="checkAll"
									name="checkAll"
									@check="handleCheckAll"
									:checked="isCheckAll === true"
								/>
							</th>
							<th class="c-table__heading">
								<span>ảnh minh họa</span>
							</th>
							<th class="c-table__heading w-250">
								<span>tên danh mục</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>chức năng</span>
							</th>
						</tr>
					</thead>
					<tbody class="c-table__body">
						<CategoryItem
							v-for="cate in category"
							:category="cate"
							@check="handleCheck"
							:checkList="state.checkList"
							:key="cate.CategoryId"
							@delete="handleDelete"
							@update="handleUpdate"
						/>
					</tbody>
				</table>
				<div
					class="c-table__empty"
					v-if="category && category.length == 0"
				>
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination v-if="pagination" :pagination="pagination" />
		</div>
	</div>

	<CategoryForm
		v-if="state.isShowForm"
		@close="handleCloseForm"
		@clear="handleClearForm"
		:CategoryId="state.CategoryId"
	/>
</template>

<style></style>
