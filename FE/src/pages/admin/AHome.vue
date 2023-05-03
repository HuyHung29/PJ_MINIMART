<script setup>
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { reactive, watch, onBeforeMount } from "vue";
import RESOURCE from "@/constants/resource";
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "order");

const { order } = useState(["order"]);

const { fetchOrder } = useActions(["fetchOrder"]);

const initData = async () => {
	await fetchOrder({ PageSize: 1000, PageNumber: 1 });
};

onBeforeMount(() => {
	initData();
});

watch(
	() => order.value,
	(newVal) => {
		handleCalculateData(newVal);
	}
);

const handleCalculateData = (arr) => {
	const _1 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 0) || [];
	const _2 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 1) || [];
	const _3 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 2) || [];
	const _4 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 3) || [];
	const _5 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 4) || [];
	const _6 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 5) || [];
	const _7 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 6) || [];
	const _8 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 7) || [];
	const _9 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 8) || [];
	const _10 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 9) || [];
	const _11 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 10) || [];
	const _12 =
		arr.filter((item) => new Date(item.CreatedDate).getMonth() == 11) || [];

	console.log(_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12);
	const t1 = _1.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t2 = _2.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t3 = _3.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t4 = _4.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t5 = _5.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t6 = _6.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t7 = _7.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t8 = _8.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t9 = _9.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t10 = _10.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t11 = _11.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);
	const t12 = _12.reduce((pre, curr) => {
		return pre + curr.Total;
	}, 0);

	state.data = [t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12];

	state.data = state.data.map((item) => {
		return item / 1000000;
	});
};

const state = reactive({
	data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
	year: 2023,
});

watch(
	() => state.data,
	(newVal) => {
		chartSeries.series[0].data = newVal;
	}
);

watch(
	() => state.year,
	(newVal) => {
		const newOrder =
			order.value.filter(
				(item) => new Date(item.CreatedDate).getFullYear() == newVal
			) || [];

		handleCalculateData(newOrder);
	}
);

const chartSeries = reactive({
	series: [{ name: "tr VNĐ", data: state.data }],
});
const chartOptions = reactive({
	chart: {
		height: 350,
		type: "line",
		zoom: {
			enabled: false,
		},
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		curve: "straight",
	},
	title: {
		text: "Doanh thu theo năm (triệu đồng)",
		align: "left",
	},
	grid: {
		row: {
			colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
			opacity: 0.5,
		},
	},
	xaxis: {
		categories: [
			"Tháng 1",
			"Tháng 2",
			"Tháng 3",
			"Tháng 4",
			"Tháng 5",
			"Tháng 6",
			"Tháng 7",
			"Tháng 8",
			"Tháng 9",
			"Tháng 10",
			"Tháng 11",
			"Tháng 12",
		],
	},
});
</script>

<template>
	<div class="data-table">
		<div class="data-table__header">
			<h2 class="data-table__heading">Tổng quan</h2>
		</div>

		<div class="c-table-wrapper">
			<div class="admin__home">
				<h2 class="admin__home__heading">Thông số về website</h2>
				<p class="admin__home__sub-heading">
					Số liệu về trang web của bạn
				</p>

				<div class="admin__home__info">
					<router-link
						to="/admin/order?Status=0"
						class="admin__home__item"
					>
						<p class="admin__home__item__number">
							{{
								order.filter(
									(item) =>
										item.Status ==
										RESOURCE.ORDER_STATUS.Waiting
								).length
							}}
						</p>
						<p class="admin__home__item__text">Chờ xác nhận</p>
					</router-link>
					<router-link
						to="/admin/order?Status=1"
						class="admin__home__item"
					>
						<p class="admin__home__item__number">
							{{
								order.filter(
									(item) =>
										item.Status ==
										RESOURCE.ORDER_STATUS.Delivered
								).length
							}}
						</p>
						<p class="admin__home__item__text">Đã vận chuyển</p>
					</router-link>
					<router-link
						to="/admin/order?Status=2"
						class="admin__home__item"
					>
						<p class="admin__home__item__number">
							{{
								order.filter(
									(item) =>
										item.Status ==
										RESOURCE.ORDER_STATUS.Done
								).length
							}}
						</p>
						<p class="admin__home__item__text">Đã giao</p>
					</router-link>
					<router-link to="/admin/order" class="admin__home__item">
						<p class="admin__home__item__number">
							{{
								(
									order.reduce((pre, curr) => {
										return pre + curr.Total;
									}, 0) / 1000000
								).toLocaleString()
							}}
							tr VNĐ
						</p>
						<p class="admin__home__item__text">Doang thu</p>
					</router-link>
					<router-link to="/admin/product" class="admin__home__item">
						<p class="admin__home__item__number">
							{{ store.state.product.pagination.TotalRecord }}
						</p>
						<p class="admin__home__item__text">Sản phẩm</p>
					</router-link>
					<router-link to="/admin/category" class="admin__home__item">
						<p class="admin__home__item__number">
							{{ store.state.category.pagination.TotalRecord }}
						</p>
						<p class="admin__home__item__text">Danh mục</p>
					</router-link>
					<router-link to="/admin/news" class="admin__home__item">
						<p class="admin__home__item__number">
							{{ store.state.news.pagination.TotalRecord }}
						</p>
						<p class="admin__home__item__text">Bài viết</p>
					</router-link>
					<router-link to="/admin/account" class="admin__home__item">
						<p class="admin__home__item__number">
							{{ store.state.user.pagination.TotalRecord }}
						</p>
						<p class="admin__home__item__text">Người dùng</p>
					</router-link>
				</div>

				<div class="admin__home__chart">
					<div class="chart-year">
						<p>Lựa chọn năm:</p>
						<Datepicker
							v-model="state.year"
							text-input
							auto-apply
							close-on-scroll
							show-now-button
							hide-offset-dates
							year-picker
						>
							<template #dp-input="{ value }">
								<div class="date-picker__wrapper">
									<input
										type="text"
										class="date-picker__input"
										:value="value"
										autocomplete="off"
									/>
									<p class="date-picker__icon">
										<i></i>
									</p>
								</div>
							</template>

							<template #now-button="{ selectCurrentDate }">
								<div class="date-picker__calendar__footer">
									<button
										class="btn btn--link"
										@click="selectCurrentDate()"
									>
										<a class="btn__text">Năm nay</a>
									</button>
								</div>
							</template>
						</Datepicker>
					</div>
					<div id="chart">
						<apexchart
							type="line"
							height="300"
							:options="chartOptions"
							:series="chartSeries.series"
						></apexchart>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.c-table-wrapper {
	max-height: calc(100vh - 150px);

	overflow-y: auto;
}

.dp__main.dp__theme_light {
	max-width: 100px;
}
.dp__input_wrap {
	max-width: 100px;
}
.date-picker__wrapper {
	max-width: 100px;
}

.admin__home__chart {
	border-top: 1px solid #ccc;
	padding: 10px 0;
	margin: 10px 0;
}

.chart-year {
	display: flex;
	align-items: center;

	margin-bottom: 20px;
}

.chart-year p {
	font-size: 14px;
	font-weight: bold;
	margin-right: 20px;
}
</style>
