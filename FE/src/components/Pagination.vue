<script setup>
import RESOURCES from "../constants/resource";
// import Select from "./customs/MSelect.vue";
import { computed, inject, reactive, watch } from "@vue/runtime-core";

/**
 * Các  hàm dùng chung
 * Author: LHH - 04/01/23
 */
const { handleGetEmployees } = inject("store");

/**
 * Các  trong component
 * Author: LHH - 04/01/23
//  */
// const pagination reactive({
// 	pageSize: 20,
// 	pageNumber: 1,
// 	siblingCount: 3,
// });

/**
 * Theo dõi sự thay đổi của  Author: LHH - 04/01/23
 */
// watch(pagination () => {
// 	try {
// 		handleGetEmployees({
// 			pageSize: pagination,
// 			pageNumber: pagination,
// 		});
// 	} catch (error) {
// 		console.log(error);
// 	}
// });

// watch(
// 	() => .pageNumber,
// 	() => {
// 		try {
// 			pagination = .pageNumber;
// 		} catch (error) {
// 			console.log(error);
// 		}
// 	}
// );

/**
 * Tính toán số trang hiển thị
 * Author: LHH -10/01/23
 */
// const pages = computed(() => {
// 	let startPage = 1;
// 	if (pagination === 1) {
// 		startPage = 1;
// 	} else if (pagination === ) {
// 		startPage =  - pagination;
// 	} else startPage = pagination - 1;
// 	const range = [];

// 	for (
// 		let i = startPage;
// 		i <=
// 		Math.min(startPage + pagination - 1, );
// 		i++
// 	) {
// 		range.push(i);
// 	}

// 	return { startPage, range };
// });

/**
 * Xử lý thay đổi số nhân viên trên 1 trang
 * Author: LHH - 04/01/23
 */
// const handleChangeEmployeeQuantity = ({ _, value }) => {
// 	try {
// 		pagination = value;
// 		pagination = 1;
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

// /**
//  * Xử lý thay chuyển trang
//  * Author: LHH - 04/01/23
//  */
// const handleChangePageNumber = (page) => {
// 	try {
// 		pagination = page;
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

// /**
//  * Xử lý ấn lùi 1 trang
//  * Author: LHH - 04/01/23
//  */
// const handleClickPreBtn = () => {
// 	try {
// 		if (pagination > 1)
// 			pagination = pagination - 1;
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

// /**
//  * Xử lý tiến 1 trang
//  * Author: LHH - 04/01/23
//  */
// const handleClickNextBtn = () => {
// 	try {
// 		if (pagination < )
// 			pagination = pagination + 1;
// 	} catch (error) {
// 		console.log(error);
// 	}
// };
</script>

<template>
	<div class="data-table__footer">
		<p class="data-table__quantity">Tổng số: <span>{{}}</span> bản ghi</p>

		<div class="data-table__action">
			<!-- <Select
				:listValue="RESOURCES.PAGINATION"
				:defaultValue="
					RESOURCES.PAGINATION.find(
						(pageSize) =>
							pageSize.value === .pageSize
					).title
				"
				:hasError="false"
				type="dropdown"
				name="emp-quantity"
				:style="{ top: 'unset', bottom: '100%' }"
				@select="handleChangeEmployeeQuantity"
			/> -->
			<!-- Pagination -->
			<div class="data-table__pagination">
				<p
					class="data-table__pagination__action"
					:class="{ disabled: 1 === 1 }"
					@click="handleClickPreBtn"
				>
					Trước
				</p>
				<ul class="data-table__pagination__page" v-show="1 > 0">
					<li
						class="data-table__pagination__item"
						@click="handleChangePageNumber(1)"
						:class="{ active: 1 === 1 }"
					>
						1
					</li>
					<!-- <li
						class="data-table__pagination__dot"
						v-if="pages.startPage > 2"
					>
						...
					</li>
					<li
						class="data-table__pagination__item"
						v-for="page in "
						:key="page"
						@click="handleChangePageNumber(page)"
						:class="{
							active: 1 === page,
						}"
						v-show="
							page !== 1 &&
							page !==  &&
							pages.range.includes(page)
						"
					>
						{{ page }}
					</li>
					<li class="data-table__pagination__dot" v-if="1">...</li>

					<li
						class="data-table__pagination__item"
						@click="handleChangePageNumber()"
						:class="{
							active:
								.pageNumber === ,
						}"
						v-show=" &&  !== 1"
					>
						{{}}
					</li> -->
				</ul>
				<p
					class="data-table__pagination__action"
					:class="{
						disabled: 0 == 0,
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
