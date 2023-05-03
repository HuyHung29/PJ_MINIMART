<script setup>
import Button from "@/components/MButton.vue";
import Pagination from "@/components/Pagination.vue";
import { reactive, onBeforeMount, computed, watch } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import OrderItem from "./components/items/OrderItem.vue";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchOrder({ ...value });
	}
);

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "order");

const { order, pagination } = useState(["order", "pagination"]);

const { fetchOrder, changeStatus } = useActions(["fetchOrder", "changeStatus"]);

const uiStore = createNamespacedHelpers(store, "ui");

const { handleOpenModal } = uiStore.useMutations(["handleOpenModal"]);

const initData = async () => {
	await fetchOrder({ ...route.query });
};

onBeforeMount(() => {
	initData();
});

const handleChangeStatus = async (item) => {
	console.log(item);
	await changeStatus(item);
};

onBeforeMount(() => {
	initData();
});

const handleSearch = (value) => {
	try {
		if (value) {
			router.push("/admin/order?Filter=" + value);
		} else {
			router.push("/admin/order");
		}
	} catch (error) {
		console.log(error);
	}
};

const state = reactive({
	active: false,
});

const status = computed(() => {
	if (route.query.Status == 0) {
		return "Chờ xác nhận";
	}
	if (route.query.Status == 1) {
		return "Đang vận chuyển";
	}
	if (route.query.Status == 2) {
		return "Đã giao";
	}

	if (route.query.Status == 3) {
		return "Hủy bỏ";
	}

	return null;
});
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Đơn hàng</h2>
		</div>

		<div class="c-table-wrapper">
			<div class="c-table__function">
				<div class="c-table__function_multiple-task">
					<button
						class="c-table__function_multiple-task__delete"
						@click="state.active = !state.active"
					>
						Trạng thái đơn hàng
					</button>
					<ul
						class="list-status"
						v-show="state.active"
						@click="state.active = false"
					>
						<li class="list-status-item">
							<router-link
								to="/admin/order"
								:class="{
									active: !route.query.Status,
								}"
							>
								Tất cả
							</router-link>
						</li>
						<li class="list-status-item">
							<router-link
								to="/admin/order?Status=0"
								:class="{
									active: route.query.Status == 0,
								}"
							>
								Chờ xác nhận
							</router-link>
						</li>
						<li class="list-status-item">
							<router-link
								to="/admin/order?Status=1"
								:class="{
									active: route.query.Status == 1,
								}"
							>
								Đã vận chuyển
							</router-link>
						</li>
						<li class="list-status-item">
							<router-link
								to="/admin/order?Status=2"
								:class="{
									active: route.query.Status == 2,
								}"
							>
								Đã giao
							</router-link>
						</li>
						<li class="list-status-item">
							<router-link
								to="/admin/order?Status=3"
								:class="{
									active: route.query.Status == 3,
								}"
							>
								Hủy bỏ
							</router-link>
						</li>
					</ul>
					<p class="c-table__function_multiple-task__text">
						{{ status }}
					</p>
				</div>
				<div class="textfield textfield--sm">
					<div class="textfield__input__wrap">
						<p class="textfield__icon">
							<i></i>
						</p>
						<input
							type="text"
							class="textfield__input"
							placeholder="Tìm kiếm đơn hàng"
							name="filter"
							:debounce-events="['input', 'keyup']"
							v-debounce:500ms.lock="handleSearch"
						/>
					</div>
				</div>
				<p class="c-table__function__refresh" @click="fetchOrder">
					<i></i>
				</p>
			</div>

			<div class="c-table__wrap">
				<table class="c-table">
					<thead class="c-table__header">
						<tr class="c-table__row">
							<th class="c-table__heading w-250">
								<span>Thông tin giao hàng</span>
							</th>
							<th class="c-table__heading w-400">
								<span>Sản phẩm</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>Ngày đặt hàng</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>Tổng tiền</span>
							</th>
							<th class="c-table__heading text-center w-400">
								<span>Địa chỉ giao hàng</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>Ghi chú</span>
							</th>
							<th class="c-table__heading text-center w-150">
								<span>Trạng thái</span>
							</th>
						</tr>
					</thead>
					<tbody class="c-table__body">
						<OrderItem
							v-for="(item, index) in order"
							:key="index"
							:order="item"
							@update="handleChangeStatus"
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
</template>

<style scoped>
.c-table__function_multiple-task__delete {
	position: relative;
}

.list-status {
	position: absolute;

	top: 100%;
	left: 0;
	right: 0;

	background-color: white;

	border: 1px solid #ccc;

	z-index: 99;
}

.list-status-item a {
	display: block;
	padding: 6px 10px;
}

.list-status-item a:hover {
	background-color: #eee;
}

.list-status-item a.active {
	background-color: #eee;
	color: orange;
}

.c-table__function_multiple-task__text {
	margin-left: 10px;
}
</style>
