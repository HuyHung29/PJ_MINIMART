<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import RESOURCES from "@/constants/resource";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import AccountItem from "./components/items/AccountItem.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchUser({ ...value });
	}
);

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"user"
);

const { listUser, pagination } = useState(["listUser", "pagination"]);

const { fetchUser, remove } = useActions(["fetchUser", "remove"]);

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
	AccountId: "",
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

	isCheck = listUser.value.every((item) =>
		state.checkList.includes(item.AccountId)
	);

	return isCheck;
});

const initData = async () => {
	await fetchUser();
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
	state.AccountId = value.AccountId;
};

const handleDelete = (value) => {
	try {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.WARNING,
			content: RESOURCES.MODAL_MESSAGE.WARNING,
			title: RESOURCES.MODAL_TITLE.WARNING,
			callback: remove,
			data: [value.AccountId],
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
		const userIds = listUser.value.map((item) => item.AccountId);

		const ids = userIds.filter((id) => !state.checkList.includes(id));
		if (target.checked) {
			state.checkList = [...state.checkList, ...ids];
		} else {
			state.checkList = [
				...state.checkList.filter((item) => !userIds.includes(item)),
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
	state.AccountId = "";
};

const handleClearForm = () => {
	state.AccountId = "";
};
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Tài khoản người dùng</h2>
		</div>

		<div class="c-table-wrapper">
			<div class="c-table__function">
				<div class="textfield textfield--sm">
					<div class="textfield__input__wrap">
						<p class="textfield__icon">
							<i></i>
						</p>
						<input
							type="text"
							class="textfield__input"
							placeholder="Tìm kiếm thông tin tài khoản"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearchEmployee"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchUser">
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
							<th class="c-table__heading w-200">
								<span>tên đăng nhập</span>
							</th>
							<th class="c-table__heading w-250">
								<span>họ tên</span>
							</th>
							<th class="c-table__heading w-250">
								<span>email</span>
							</th>
							<th class="c-table__heading w-250">
								<span>số điện thoại</span>
							</th>
							<th class="c-table__heading w-250">
								<span>giới tính</span>
							</th>
							<th class="c-table__heading w-250">
								<span>ngày sinh</span>
							</th>
							<th class="c-table__heading w-250">
								<span>địa chỉ</span>
							</th>
							<!-- <th class="c-table__heading text-center w-150">
								<span>chức năng</span>
							</th> -->
						</tr>
					</thead>
					<tbody class="c-table__body">
						<AccountItem
							v-for="user in listUser"
							:user="user"
							@check="handleCheck"
							:checkList="state.checkList"
							:key="user.AccountId"
							@delete="handleDelete"
							@update="handleUpdate"
						/>
					</tbody>
				</table>
				<div
					class="c-table__empty"
					v-if="listUser && listUser.length == 0"
				>
					<img src="@/assets/images/nodata.76e50bd8.svg" alt="" />
					<p>Không có dữ liệu</p>
				</div>
			</div>

			<Pagination v-if="pagination" :pagination="pagination" />
		</div>
	</div>
</template>

<style></style>
