<script setup>
import SupplierItem from "./components/SupplierItem.vue";
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

/**
 * Call API
 * Author: LHH - 04/01/23
 */
onBeforeMount(() => {
	try {
		// handleGetAllEmployee();
	} catch (error) {
		console.log(error);
	}
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
		const empIds = state.employees.map((item) => item.EmployeeId);

		const ids = empIds.filter((id) => !empState.checkList.includes(id));
		if (target.checked) {
			empState.checkList = [...empState.checkList, ...ids];
		} else {
			empState.checkList = [
				...empState.checkList.filter((item) => !empIds.includes(item)),
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
			<h2 class="data-table__heading">Mã giảm giá</h2>
			<Button
				content="Thêm mới mã giảm giá"
				@click=""
				tooltip="Ctrl + 1"
			/>
		</div>

		<div class="table-wrapper">
			<div class="table__function">
				<div
					class="table__function_multiple-task"
					v-show="empState.checkList.length >= 1"
				>
					<p class="table__function_multiple-task__text">
						Đã chọn {{ empState.checkList.length }}
					</p>
					<p
						class="table__function_multiple-task__text warning"
						@click="empState.checkList = []"
					>
						Bỏ chọn
					</p>

					<button
						class="table__function_multiple-task__delete"
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
				<p
					class="table__function__refresh"
					@click="handleOpenLoading"
					:debounce-events="['click']"
					v-debounce:500ms.lock="handleRefreshData"
				>
					<i></i>
				</p>
				<p class="table__function__export" @click="handleExportData">
					<i></i>
				</p>
			</div>

			<div class="table__wrap" @scroll="handleCloseListAction">
				<table class="table">
					<thead class="table__header">
						<tr class="table__row">
							<th class="table__heading text-center">
								<CheckBox
									id="checkAll"
									name="checkAll"
									@check="handleCheckAll"
									:checked="isCheckAll === true"
								/>
							</th>
							<th class="table__heading w-150">
								<span>mã nhân viên</span>
							</th>
							<th class="table__heading w-250">
								<span>tên nhân viên</span>
							</th>
							<th class="table__heading w-100">
								<span>giới tính</span>
							</th>
							<th class="table__heading text-center w-200">
								<span>ngày sinh</span>
							</th>
							<th class="table__heading w-200">
								<span>số cmnd</span>
								<p class="table__heading__tooltip">
									Số chứng minh nhân dân
								</p>
							</th>
							<th class="table__heading w-200">
								<span>chức danh</span>
							</th>
							<th class="table__heading w-300">
								<span>tên đơn vị</span>
							</th>
							<th class="table__heading w-200">
								<span>số điện thoại</span>
							</th>
							<th class="table__heading w-200">
								<span>số tài khoản</span>
							</th>
							<th class="table__heading w-200">
								<span>tên ngân hàng</span>
							</th>
							<th class="table__heading w-200">
								<span>chi nhánh</span>
							</th>
							<th class="table__heading text-center w-150">
								<span>chức năng</span>
							</th>
						</tr>
					</thead>
					<tbody class="table__body">
						<SupplierItem
							v-for="employee in 0"
							:employee="employee"
							@click="handleDisplayListAction"
							@check="handleCheck"
							@closeList="handleCloseListOutside"
							:checkList="empState.checkList"
							:key="employee.EmployeeId"
						/>
					</tbody>
				</table>
				<!-- Action list -->
				<ul
					class="table__action__list"
					:style="empState.listAction.style"
					v-if="empState.listAction.isShow"
					ref="listActionRef"
				>
					<li
						class="table__action__item"
						@mousedown="handleDuplicateClick"
					>
						Nhân bản
					</li>
					<li
						class="table__action__item open-dialog-btn"
						@mousedown="onDeleteBtnClick"
					>
						Xóa
					</li>
					<li
						class="table__action__item blocked"
						@mousedown="handleCloseListAction"
					>
						Ngưng sử dụng
					</li>
				</ul>
				<div class="table__empty" v-if="0 === 0">
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination />
		</div>
	</div>
</template>

<style></style>
