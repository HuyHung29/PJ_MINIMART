<script setup>
import RESOURCE from "@/constants/resource";
import { useStore } from "vuex";
import { onBeforeMount, watch, reactive } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import MButton from "@/components/MButton.vue";
import { useRoute, useRouter } from "vue-router";

const route = useRoute();
const router = useRouter();

watch(
	() => route.query,
	(value) => {
		fetchOrderUser({ ...value });
	}
);

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "order");

const { order } = useState(["order"]);

const { fetchOrderUser, changeStatus } = useActions([
	"fetchOrderUser",
	"changeStatus",
]);

const uiStore = createNamespacedHelpers(store, "ui");

const { handleOpenModal } = uiStore.useMutations(["handleOpenModal"]);

const initData = async () => {
	await fetchOrderUser({ ...route.query });
};

onBeforeMount(() => {
	initData();
});

const handleCancelOrder = async (item) => {
	handleOpenModal({
		type: RESOURCE.MODAL_TYPE.WARNING,
		content: "Bạn có chắc chắn muốn hủy đơn hàng này không?",
		title: "Xác nhận hủy",
		callback: changeStatus,
		data: {
			OrderId: item.OrderId,
			AccountId: item.AccountId,
			Products: item.Products,
			Status: RESOURCE.ORDER_STATUS.Cancled,
		},
	});
};
</script>

<template>
	<div class="user__purchase">
		<div class="user__purchase__header">
			<ul class="user__purchase__header__tab">
				<li class="user__purchase__header__item">
					<router-link
						to="/user/order"
						class="user__purchase__header__link"
						:class="{
							active: !route.query.Status,
						}"
					>
						Tất cả
					</router-link>
				</li>
				<li class="user__purchase__header__item">
					<router-link
						to="/user/order?Status=0"
						class="user__purchase__header__link"
						:class="{
							active: route.query.Status == 0,
						}"
					>
						Chờ xác nhận
					</router-link>
				</li>
				<li class="user__purchase__header__item">
					<router-link
						to="/user/order?Status=1"
						class="user__purchase__header__link"
						:class="{
							active: route.query.Status == 1,
						}"
					>
						Đã vận chuyển
					</router-link>
				</li>
				<li class="user__purchase__header__item">
					<router-link
						to="/user/order?Status=2"
						class="user__purchase__header__link"
						:class="{
							active: route.query.Status == 2,
						}"
					>
						Đã giao
					</router-link>
				</li>
				<li class="user__purchase__header__item">
					<router-link
						to="/user/order?Status=3"
						class="user__purchase__header__link"
						:class="{
							active: route.query.Status == 3,
						}"
					>
						Hủy bỏ
					</router-link>
				</li>

				<div
					class="user__purchase__header__line"
					style="{active.style}"
				></div>
			</ul>
		</div>

		<div class="user__purchase__list">
			<div class="user__purchase--empty" v-if="order.length === 0">
				<img src="@/assets/images/list-empty.png" alt="anh" />
				<p>Không có đơn hàng nào</p>
			</div>
			<div
				class="user__purchase__item"
				v-for="(item, index) in order"
				:key="index"
			>
				<div class="user__purchase-header">
					<p class="user__purchase-header__address">
						Người nhận: {{ item.Receiver }} <br />
						Số điện thoại: {{ item.PhoneNumber }} <br />
						Địa chỉ: {{ item.DeliveryPlace }}
					</p>
					<p
						:class="{
							user__purchase__item__status: true,
							_0: item.Status === RESOURCE.ORDER_STATUS.Waiting,
							_1: item.Status === RESOURCE.ORDER_STATUS.Delivered,
							_2: item.Status === RESOURCE.ORDER_STATUS.Done,
						}"
					>
						{{ item.StatusName }}
					</p>
				</div>
				<div class="user__purchase__product">
					<div
						class="user__purchase__product__item"
						v-for="(pro, ind) in item.Products"
						:key="ind"
					>
						<img
							:src="pro.Url"
							alt="anh"
							class="user__purchase__product__item__thumbnail"
						/>
						<div class="user__purchase__product__item__info">
							<p class="user__purchase__product__item__name">
								{{ pro.ProductName }}
							</p>
							<p class="user__purchase__product__item__quantity">
								x{{ pro.Quantity }}
							</p>
						</div>
						<p class="user__purchase__product__item__total">
							{{ pro.Price.toLocaleString() }}
							<sup>đ</sup>
						</p>
					</div>
				</div>
				<div class="user__purchase__item__action">
					<MButton
						content="Hủy đơn hàng"
						v-if="item.Status === RESOURCE.ORDER_STATUS.Waiting"
						@click="() => handleCancelOrder(item)"
					/>
					<p class="user__purchase__item__sum">
						Tổng số tiền:
						<span>
							{{ item.Total.toLocaleString() }}
							<sup>đ</sup>
						</span>
					</p>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.user__purchase__product {
	max-height: 200px;
	overflow-y: auto;
}

.user__purchase__list {
	max-height: 600px;
	overflow-y: auto;
}

.user__purchase-header {
	display: flex;
	justify-content: space-between;
	align-items: center;
	border-bottom: 1px solid #e9e9e9;
	padding-bottom: 10px;
	margin-bottom: 15px;
}
.user__purchase__item__status {
	border-bottom: unset;
	padding-bottom: unset;
	margin-bottom: unset;
}

.user__purchase-header__address {
	font-size: 14px;
	line-height: 15px;
}
</style>
