<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import SupplierForm from "./components/forms/SupplierForm.vue";
import SupplierItem from "./components/items/SupplierItem.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchSupplier({ ...value });
	}
);

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"supplier"
);

const { supplier, pagination } = useState(["supplier", "pagination"]);

const { fetchSupplier, remove } = useActions(["fetchSupplier", "remove"]);

const { listSupp = supplier } = useGetters(["supplier"]);

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
	SupplierId: "",
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

	isCheck = supplier._value.every((item) =>
		state.checkList.includes(item.SupplierId)
	);

	return isCheck;
});

const initData = async () => {
	await fetchSupplier();
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
	state.SupplierId = value.SupplierId;
};

const handleDelete = (value) => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [value.SupplierId],
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
		const suppIds = listSupp._value.map((item) => item.SupplierId);

		const ids = suppIds.filter((id) => !state.checkList.includes(id));
		if (target.checked) {
			state.checkList = [...state.checkList, ...ids];
		} else {
			state.checkList = [
				...state.checkList.filter((item) => !suppIds.includes(item)),
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
			await fetchSupplier({ Filter: value, PageNumber: 1 });
		} else {
			await fetchSupplier({ Filter: "", PageNumber: 1 });
		}
	} catch (error) {
		console.log(error);
	}
};

const handleCloseForm = () => {
	state.isShowForm = false;
	state.SupplierId = "";
};

const handleClearForm = () => {
	state.SupplierId = "";
};
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Nhà cung cấp</h2>
			<Button
				content="Thêm mới nhà cung cấp"
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
							placeholder="Tìm theo mã, tên nhà cung cấp"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearch"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchSupplier">
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
								<span>Mã nhà cung cấp</span>
							</th>
							<th class="c-table__heading w-250">
								<span>tên nhà cung cấp</span>
							</th>
							<th class="c-table__heading w-250">
								<span>địa chỉ</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>chức năng</span>
							</th>
						</tr>
					</thead>
					<tbody class="c-table__body">
						<SupplierItem
							v-for="supp in supplier"
							:supplier="supp"
							@check="handleCheck"
							:checkList="state.checkList"
							:key="supp.SupplierId"
							@delete="handleDelete"
							@update="handleUpdate"
						/>
					</tbody>
				</table>
				<div
					class="c-table__empty"
					v-if="supplier && supplier.length == 0"
				>
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination v-if="pagination" :pagination="pagination" />
		</div>
	</div>

	<SupplierForm
		v-if="state.isShowForm"
		@close="handleCloseForm"
		@clear="handleClearForm"
		:SupplierId="state.SupplierId"
	/>
</template>

<style></style>
