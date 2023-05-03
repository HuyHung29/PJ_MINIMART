<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import NewsForm from "./components/forms/NewsForm.vue";
import NewsItem from "./components/items/NewsItem.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchNews({ ...value });
	}
);

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"news"
);

const { news, pagination } = useState(["news", "pagination"]);

const { fetchNews, remove, getNews } = useActions([
	"fetchNews",
	"remove",
	"getNews",
]);

const { listNews = news } = useGetters(["news"]);

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
	NewsId: "",
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

	isCheck = news._value.every((item) =>
		state.checkList.includes(item.NewsId)
	);

	return isCheck;
});

const initData = async () => {
	await fetchNews();
};

/**
 * Call API
 * Author: LHH - 04/01/23
 */
onBeforeMount(() => {
	initData();
});

const handleUpdate = async (value) => {
	console.log(value);
	await getNews(value.NewsId);
	state.isShowForm = true;
	state.NewsId = value.NewsId;
};

const handleDelete = (value) => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [value.NewsId],
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
		const cateIds = listNews._value.map((item) => item.NewsId);

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
const handleSearch = async (value) => {
	try {
		if (value) {
			await fetchNews({ Filter: value });
		} else {
			await fetchNews({ Filter: "", PageNumber: 1 });
		}
	} catch (error) {
		console.log(error);
	}
};

const handleCloseForm = () => {
	state.isShowForm = false;
	state.NewsId = "";
};

const handleClearForm = () => {
	state.NewsId = "";
};
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Tin tức</h2>
			<Button
				content="Thêm mới tin tức"
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
							placeholder="Tìm theo tên bài viết"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearch"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchNews">
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
								<span>tên bài viết</span>
							</th>
							<th class="c-table__heading w-250">
								<span>ngày tạo</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>chức năng</span>
							</th>
						</tr>
					</thead>
					<tbody class="c-table__body">
						<NewsItem
							v-for="cate in news"
							:news="cate"
							@check="handleCheck"
							:checkList="state.checkList"
							:key="cate.NewsId"
							@delete="handleDelete"
							@update="handleUpdate"
						/>
					</tbody>
				</table>
				<div class="c-table__empty" v-if="news && news.length == 0">
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination v-if="pagination" :pagination="pagination" />
		</div>
	</div>

	<NewsForm
		v-if="state.isShowForm"
		@close="handleCloseForm"
		@clear="handleClearForm"
		:NewsId="state.NewsId"
	/>
</template>

<style></style>
