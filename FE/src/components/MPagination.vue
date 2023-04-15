<script setup>
import RESOURCES from "../constants/resource";
import Select from "./customs/MSelect.vue";
import { computed, inject, reactive, watch } from "@vue/runtime-core";

/**
 * Các state và hàm dùng chung
 * Author: LHH - 04/01/23
 */
const { state, handleGetEmployees } = inject("store");

/**
 * Các state dùng trong component
 * Author: LHH - 04/01/23
 */
const paginationState = reactive({
	pageSize: 20,
	pageNumber: 1,
	siblingCount: 3,
});

/**
 * Theo dõi sự thay đổi của state
 * Author: LHH - 04/01/23
 */
watch(paginationState, () => {
	try {
		handleGetEmployees({
			pageSize: paginationState.pageSize,
			pageNumber: paginationState.pageNumber,
		});
	} catch (error) {
		console.log(error);
	}
});

watch(
	() => state.pagination.pageNumber,
	() => {
		try {
			paginationState.pageNumber = state.pagination.pageNumber;
		} catch (error) {
			console.log(error);
		}
	}
);

/**
 * Tính toán số trang hiển thị
 * Author: LHH -10/01/23
 */
const pages = computed(() => {
	let startPage = 1;
	if (paginationState.pageNumber === 1) {
		startPage = 1;
	} else if (paginationState.pageNumber === state.totalPage) {
		startPage = state.totalPage - paginationState.siblingCount;
	} else startPage = paginationState.pageNumber - 1;
	const range = [];

	for (
		let i = startPage;
		i <=
		Math.min(startPage + paginationState.siblingCount - 1, state.totalPage);
		i++
	) {
		range.push(i);
	}

	return { startPage, range };
});

/**
 * Xử lý thay đổi số nhân viên trên 1 trang
 * Author: LHH - 04/01/23
 */
const handleChangeEmployeeQuantity = ({ _, value }) => {
	try {
		paginationState.pageSize = value;
		paginationState.pageNumber = 1;
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý thay chuyển trang
 * Author: LHH - 04/01/23
 */
const handleChangePageNumber = (page) => {
	try {
		paginationState.pageNumber = page;
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý ấn lùi 1 trang
 * Author: LHH - 04/01/23
 */
const handleClickPreBtn = () => {
	try {
		if (paginationState.pageNumber > 1)
			paginationState.pageNumber = paginationState.pageNumber - 1;
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý tiến 1 trang
 * Author: LHH - 04/01/23
 */
const handleClickNextBtn = () => {
	try {
		if (paginationState.pageNumber < state.totalPage)
			paginationState.pageNumber = paginationState.pageNumber + 1;
	} catch (error) {
		console.log(error);
	}
};
</script>

<template>
	<div class="data-table__footer">
		<p class="data-table__quantity">
			Tổng số: <span>{{ state.totalRecord }}</span> bản ghi
		</p>

		<div class="data-table__action">
			<Select
				:listValue="RESOURCES.PAGINATION"
				:defaultValue="
					RESOURCES.PAGINATION.find(
						(pageSize) =>
							pageSize.value === state.pagination.pageSize
					).title
				"
				:hasError="false"
				type="dropdown"
				name="emp-quantity"
				:style="{ top: 'unset', bottom: '100%' }"
				@select="handleChangeEmployeeQuantity"
			/>
			<!-- Pagination -->
			<div class="data-table__pagination">
				<p
					class="data-table__pagination__action"
					:class="{ disabled: paginationState.pageNumber === 1 }"
					@click="handleClickPreBtn"
				>
					Trước
				</p>
				<ul
					class="data-table__pagination__page"
					v-show="state.totalRecord > 0"
				>
					<li
						class="data-table__pagination__item"
						@click="handleChangePageNumber(1)"
						:class="{ active: state.pagination.pageNumber === 1 }"
					>
						1
					</li>
					<li
						class="data-table__pagination__dot"
						v-if="pages.startPage > 2"
					>
						...
					</li>
					<li
						class="data-table__pagination__item"
						v-for="page in state.totalPage"
						:key="page"
						@click="handleChangePageNumber(page)"
						:class="{
							active: state.pagination.pageNumber === page,
						}"
						v-show="
							page !== 1 &&
							page !== state.totalPage &&
							pages.range.includes(page)
						"
					>
						{{ page }}
					</li>
					<li
						class="data-table__pagination__dot"
						v-if="
							pages.startPage + paginationState.siblingCount <
							state.totalPage
						"
					>
						...
					</li>

					<li
						class="data-table__pagination__item"
						@click="handleChangePageNumber(state.totalPage)"
						:class="{
							active:
								state.pagination.pageNumber === state.totalPage,
						}"
						v-show="state.totalPage && state.totalPage !== 1"
					>
						{{ state.totalPage }}
					</li>
				</ul>
				<p
					class="data-table__pagination__action"
					:class="{
						disabled:
							paginationState.pageNumber === state.totalPage ||
							state.totalPage === 0,
					}"
					@click="handleClickNextBtn"
				>
					Sau
				</p>
			</div>
		</div>
	</div>
</template>

<style></style>
