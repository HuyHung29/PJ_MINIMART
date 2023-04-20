<script setup>
import CategoryItem from "./components/CategoryItem.vue";
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import {
	reactive,
	inject,
	onBeforeMount,
	computed,
	ref,
	onMounted,
	onBeforeUnmount,
} from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"category"
);

const { category, pagination } = useState(["category", "pagination"]);

const { fetchCategory } = useActions(["fetchCategory"]);

const { listCate = category } = useGetters(["category"]);

/**
 * Định nghĩa các state
 * Author: LHH - 04/01/23
 */
const empState = reactive({
	listAction: {
		isShow: true,
		employeeId: "",
		employeeCode: "",
		style: {
			top: "",
			right: "",
		},
	},
	listActionHeight: 0,
	checkList: [],
});

/**
 * Định nghĩa các ref của component
 * Author: LHH - 04/01/23
 */
const listActionRef = ref(null);

/**
 * Hàm xử lý check all
 * Author: LHH - 14/02/23
 */
const isCheckAll = computed(() => {
	let isCheck = true;

	if (empState.checkList.length === 0) {
		return false;
	}

	// isCheck = state.employees.every((item) =>
	// 	empState.checkList.includes(item.EmployeeId)
	// );

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

/**
 * Xử lý lấy chiều cao của list action và các phím tắt
 * Author: LHH - 04/01/23
 */
onMounted(() => {
	// Lấy chiều cao của list action
	empState.listActionHeight = listActionRef.value.offsetHeight;
	empState.listAction.isShow = false;

	// Xử lý các phím tắt
	document.addEventListener("keydown", handleKeyDownEvent);
});

/**
 * Xử lý hủy bô các sự kiện
 * Author: LHH - 27/01/23
 */
onBeforeUnmount(() => {
	document.removeEventListener("keydown", handleKeyDownEvent);
});

/**
 * Xử lý các phím tắt trên màn hình danh sách
 * @param {event} e đối tượng event của js
 * Author: LHH - 27/02/23
 */
const handleKeyDownEvent = (e) => {
	// Xử lý phím tắt cho nút thêm mới

	if (e.ctrlKey && e.keyCode === RESOURCES.KEYCODE.NUMBER_ONE) {
		e.preventDefault();
		e.stopPropagation();

		handleOpenAddForm();
	}
};

/**
 * Hàm xử lý việc hiển thị action list
 * Author: LHH -
 */
const handleDisplayListAction = (data) => {
	try {
		empState.listAction.isShow = !empState.listAction.isShow;

		const isOutWindow =
			data.top + data.height + empState.listActionHeight <
			window.innerHeight;

		empState.listAction = {
			isShow: empState.listAction.isShow,
			employeeId: data.employeeId,
			employeeCode: data.employeeCode,
			style: {
				top: isOutWindow
					? data.top + data.height + "px"
					: data.top - empState.listActionHeight + "px",
				right: data.right - data.width + "px",
			},
		};
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý đóng action
 * Author: LHH - 04/01/23
 */
const handleCloseListAction = () => {
	try {
		empState.listAction.isShow = false;
	} catch (error) {
		console.log(error);
	}
};

const handleCloseListOutside = () => {
	if (empState.listAction.isShow) {
		empState.listAction.isShow = false;
	}
};

/**
 * Xử lý ấn nút nhân bản
 * Author: LHH - 11/01/23
 */
// const handleDuplicateClick = () => {
// 	try {
// 		empState.listAction.isShow = false;
// 		handleOpenForm(
// 			RESOURCES.FORM_MODE.DUPLICATE,
// 			empState.listAction.employeeId
// 		);
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Xử lý xóa nhân viên
 * Author: LHH - 30/01/23
//  */
// const handleDeleteEmployee = async () => {
// 	try {
// 		await deleteEmployee([...state.modal.employeeId]);

// 		if (statusCode.value) {
// 			handleUpdateEmployeeList(
// 				"DELETE",
// 				state.modal.employeeId,
// 				deleteIds.value
// 			);

// 			handleShowToast({
// 				type: RESOURCES.NOTIFICATION_TYPE.SUCCESS,
// 				content:
// 					state.modal.type === RESOURCES.MODAL_TYPE.WARNING
// 						? RESOURCES.NOTIFICATION_MESSAGE.SUCCESS.DELETE
// 						: RESOURCES.NOTIFICATION_MESSAGE.SUCCESS[
// 								state.form.type
// 						  ],
// 			});
// 			handleCloseModal();

// 			empState.checkList = empState.checkList.filter(
// 				(id) => !deleteIds.value.includes(id)
// 			);
// 		}
// 	} catch (error) {
// 		console.log(error);
// 		const { UserMes } = error;
// 		handleShowToast({
// 			type: RESOURCES.NOTIFICATION_TYPE.ERROR,
// 			content: UserMes,
// 		});
// 		handleCloseModal();
// 	}
// };

/**
 * Hàm xử lý khi ấn nút xóa
 * Author: LHH - 04/01/23
 */
// const onDeleteBtnClick = () => {
// 	try {
// 		handleOpenModal(
// 			RESOURCES.MODAL_TITLE.WARNING,
// 			RESOURCES.MODAL_MESSAGE.WARNING(empState.listAction.employeeCode),
// 			RESOURCES.MODAL_TYPE.WARNING,
// 			[empState.listAction.employeeId],
// 			handleDeleteEmployee
// 		);

// 		handleCloseListAction();
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Hàm xử lý khi ấn nút xóa hàng loạt
 * Author: LHH - 17/02/23
 */
// const handleMultipleDelete = () => {
// 	handleOpenModal(
// 		RESOURCES.MODAL_TITLE.WARNING,
// 		RESOURCES.MODAL_MESSAGE.WARNING_MULTIPLE,
// 		RESOURCES.MODAL_TYPE.WARNING,
// 		[...empState.checkList],
// 		handleDeleteEmployee
// 	);
// };

/**
 * Hàm xử lý check all
 * Author: LHH - 04/01/23
 */
const handleCheckAll = (target) => {
	try {
		const cateIds = listCate._value.map((item) => item.CategoryId);

		const ids = cateIds.filter((id) => !empState.checkList.includes(id));
		if (target.checked) {
			empState.checkList = [...empState.checkList, ...ids];
		} else {
			empState.checkList = [
				...empState.checkList.filter((item) => !cateIds.includes(item)),
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
		if (empState.checkList.includes(value)) {
			empState.checkList.splice(empState.checkList.indexOf(value), 1);
		} else {
			empState.checkList.push(value);
		}
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý tìm kiếm
 * Author: LHH - 04/01/23
 */
// const handleSearchEmployee = (value) => {
// 	try {
// 		if (value) {
// 			handleGetEmployees({ keyword: value });
// 		} else {
// 			handleGetEmployees();
// 		}
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Hàm xử lý refresh dữ liệu
 * Author: LHH - 04/01/23
 */
// const handleRefreshData = () => {
// 	try {
// 		handleGetEmployees({ ...state.pagination });
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Hàm xử lý mở form thêm mới
 * Author: LHH - 04/01/2023
 */
// const handleOpenAddForm = () => {
// 	handleOpenForm(RESOURCES.FORM_MODE.ADD);
// };

/**
 * Hàm xử lý xuất file excel
 * Author: LHH - 04/01/2023
 */
// const handleExportData = async () => {
// 	try {
// 		const { NOTIFICATION_TYPE, NOTIFICATION_MESSAGE } = RESOURCES;
// 		handleOpenLoading();

// 		await handleExportExcel({
// 			keyword: state.pagination.keyword ? state.pagination.keyword : null,
// 			pageSize: state.totalRecord,
// 			pageNumber: 1,
// 		});

// 		handleShowToast({
// 			type: NOTIFICATION_TYPE.SUCCESS,
// 			content: NOTIFICATION_MESSAGE.SUCCESS.EXPORT,
// 		});

// 		handleCloseLoading();
// 	} catch (error) {
// 		console.log(error);
// 		handleCloseLoading();
// 	}
// };
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Danh mục</h2>
			<Button content="Thêm mới danh mục" @click="" tooltip="Ctrl + 1" />
		</div>

		<div class="c-table-wrapper">
			<div class="c-table__function">
				<div
					class="c-table__function_multiple-task"
					v-show="empState.checkList.length >= 1"
				>
					<p class="c-table__function_multiple-task__text">
						Đã chọn {{ empState.checkList.length }}
					</p>
					<p
						class="c-table__function_multiple-task__text warning"
						@click="empState.checkList = []"
					>
						Bỏ chọn
					</p>

					<button
						class="c-table__function_multiple-task__delete"
						@click="handleMultipleDelete"
						:disabled="empState.checkList.length < 1"
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

			<div class="c-table__wrap" @scroll="handleCloseListAction">
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
							@click="handleDisplayListAction"
							@check="handleCheck"
							@closeList="handleCloseListOutside"
							:checkList="empState.checkList"
							:key="cate.CategoryId"
						/>
					</tbody>
				</table>
				<!-- Action list -->
				<ul
					class="c-table__action__list"
					:style="empState.listAction.style"
					v-if="empState.listAction.isShow"
					ref="listActionRef"
				>
					<li
						class="c-table__action__item"
						@mousedown="handleDuplicateClick"
					>
						Nhân bản
					</li>
					<li
						class="c-table__action__item open-dialog-btn"
						@mousedown="onDeleteBtnClick"
					>
						Xóa
					</li>
					<li
						class="c-table__action__item blocked"
						@mousedown="handleCloseListAction"
					>
						Ngưng sử dụng
					</li>
				</ul>
				<div
					class="c-table__empty"
					v-if="category && category.length == 0"
				>
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination :pagination="pagination" />
		</div>
	</div>
</template>

<style></style>
