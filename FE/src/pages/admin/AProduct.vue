<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import ProductItem from "./components/items/ProductItem.vue";
import ProductForm from "./components/forms/ProductForm.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchProduct({ ...value });
	}
);

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"product"
);

const { product, pagination } = useState(["product", "pagination"]);

const { fetchProduct, remove, getProduct } = useActions([
	"fetchProduct",
	"remove",
	"getProduct",
]);

const { listPro = product } = useGetters(["product"]);

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
	ProductId: "",
	active: false,
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

	isCheck = product._value.every((item) =>
		state.checkList.includes(item.ProductId)
	);

	return isCheck;
});

const initData = async () => {
	await fetchProduct();
};

/**
 * Call API
 * Author: LHH - 04/01/23
 */
onBeforeMount(() => {
	initData();
});

const handleUpdate = async (value) => {
	await getProduct(value.ProductId);
	state.isShowForm = true;
	state.ProductId = value.ProductId;
};

const handleDelete = (value) => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [value.ProductId],
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
		const proIds = listPro._value.map((item) => item.ProductId);

		const ids = proIds.filter((id) => !state.checkList.includes(id));
		if (target.checked) {
			state.checkList = [...state.checkList, ...ids];
		} else {
			state.checkList = [
				...state.checkList.filter((item) => !proIds.includes(item)),
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
			router.push("/admin/product?Filter=" + value);
			// await fetchProduct({ Filter: value, PageNumber: 1 });
		} else {
			router.push("/admin/product");
			// await fetchProduct({ Filter: "", PageNumber: 1 });
		}
	} catch (error) {
		console.log(error);
	}
};

const handleCloseForm = () => {
	state.isShowForm = false;
	state.ProductId = "";
};

const handleClearForm = () => {
	state.ProductId = "";
};

const handleGetProductOFT = () => {
	if (state.active) {
		router.push("/admin/product");
	} else {
		router.push("/admin/product?Quantity=0");
	}
	state.active = !state.active;
};
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Sản phẩm</h2>
			<Button
				content="Thêm mới sản phẩm"
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
				<button
					class="c-table__function_multiple-task__delete get-product"
					:class="{ active: state.active }"
					@click="handleGetProductOFT"
				>
					Sản phẩm hết hàng
				</button>
				<div class="textfield textfield--sm">
					<div class="textfield__input__wrap">
						<p class="textfield__icon">
							<i></i>
						</p>
						<input
							type="text"
							class="textfield__input"
							placeholder="Tìm theo mã, tên sản phẩm"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearch"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchProduct">
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
							<th class="c-table__heading w-150">
								<span>Mã sản phẩm</span>
							</th>
							<th class="c-table__heading w-200">
								<span>Tên sản phẩm</span>
							</th>
							<th class="c-table__heading w-400">
								<span>Ảnh minh họa</span>
							</th>
							<th class="c-table__heading w-150">
								<span>Giá</span>
							</th>
							<th class="c-table__heading c-w-100">
								<span>Số lượng</span>
							</th>
							<th class="c-table__heading c-w-100">
								<span>Đơn vị tính</span>
							</th>
							<th class="c-table__heading w-200">
								<span>Khối lượng/Dung tích</span>
							</th>
							<th class="c-table__heading w-200">
								<span>Danh mục</span>
							</th>
							<th class="c-table__heading w-200">
								<span>Nhà cung cấp</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>chức năng</span>
							</th>
						</tr>
					</thead>
					<tbody class="c-table__body">
						<ProductItem
							v-for="pro in product"
							:product="pro"
							@check="handleCheck"
							:checkList="state.checkList"
							:key="pro.ProductId"
							@delete="handleDelete"
							@update="handleUpdate"
						/>
					</tbody>
				</table>
				<div
					class="c-table__empty"
					v-if="product && product.length == 0"
				>
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination v-if="pagination" :pagination="pagination" />
		</div>
	</div>

	<ProductForm
		v-if="state.isShowForm"
		@close="handleCloseForm"
		@clear="handleClearForm"
		:ProductId="state.ProductId"
	/>
</template>

<style scoped>
.c-table__function_multiple-task__delete.get-product {
	margin-right: 10px;
}

.c-table__function_multiple-task__delete.active {
	background-color: #ccc;
}
</style>
