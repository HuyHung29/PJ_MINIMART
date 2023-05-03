<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount, computed, reactive } from "vue";
import CartPageItem from "./components/CartPageItem.vue";
import MCheckBox from "@/components/customs/MCheckBox.vue";

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"cart"
);

const { fetchCart, remove, update } = useActions([
	"fetchCart",
	"remove",
	"update",
]);

const { cart } = useState(["cart"]);

const state = reactive({
	checkList: [],
});

const total = computed(() => {
	return cart.value
		? cart?.value?.reduce((prev, curr) => {
				return prev + curr.CurrentPrice * curr.Quantity;
		  }, 0)
		: 0;
});

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

const handleCheckAll = (target) => {
	try {
		const proIds = cart.value.map((item) => item.ProductId);

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

const isCheckAll = computed(() => {
	let isCheck = true;

	if (state.checkList.length === 0) {
		return false;
	}

	isCheck = cart.value.every((item) =>
		state.checkList.includes(item.ProductId)
	);

	return isCheck;
});

const handleUpdate = async (value) => {
	console.log(value);
	await update(value);
};

const handleDeleteItem = async (id) => {
	await remove([id]);
};

const handleDeleteMultiple = async () => {
	await remove([...state.checkList]);
};
</script>

<template>
	<Container class="cart-page">
		<Row>
			<Col md="12">
				<div class="cart-page--empty" v-if="cart.length == 0">
					<img src="@/assets/images/emptyCart.png" alt="empty" />
					<p>Giỏ hàng của bạn trống</p>
					<router-link to="/product" class="btn-normal">
						Mua ngay
					</router-link>
				</div>

				<div
					class="cart-page__item cart-page--margin bg-white"
					v-if="cart && cart.length > 0"
				>
					<div class="cart-page__item__checkbox">
						<MCheckBox
							name="checkAll"
							id="checkAll"
							@check="handleCheckAll"
							:checked="isCheckAll === true"
						/>
					</div>
					<div class="cart-page__item__product">Sản phẩm</div>
					<div class="cart-page__item__price">Đơn giá</div>
					<div class="cart-page__item__quantity">Số lượng</div>
					<div class="cart-page__item__total">Thành tiền</div>
					<div class="cart-page__item__action">Thao tác</div>
				</div>
				<div
					class="cart-page__list bg-white"
					v-if="cart && cart.length > 0"
				>
					<CartPageItem
						v-for="(item, index) in cart"
						:key="index"
						:product="item"
						:checkList="state.checkList"
						@check="handleCheck"
						@update="handleUpdate"
						@delete="handleDeleteItem"
					/>
				</div>
				<div
					class="cart-page__item cart-page--margin bg-white"
					v-if="cart && cart.length > 0"
				>
					<div class="cart-page__item__checkbox"></div>
					<div class="cart-page__item__footer-action">
						<label htmlFor="checkAll"> Chọn tất cả </label>
						<p
							class="delete-multiple"
							v-if="state.checkList.length > 0"
							@click="handleDeleteMultiple"
						>
							Xóa những sản phẩm được chọn
						</p>
					</div>
					<div class="cart-page__item__summary">
						<p>
							Tổng thanh toán ({{ cart.length }} Sản phẩm):
							<span>
								<sup>đ</sup>
								{{ total.toLocaleString() }}
							</span>
						</p>
						<router-link to="/purchase" class="flex-grow-1">
							<button class="btn buy-btn mt-0 w-100 shadow-none">
								Đặt hàng
							</button>
						</router-link>
					</div>
				</div>
			</Col>
		</Row>
	</Container>
</template>

<style scoped>
.delete-multiple {
	color: red;
}

.cart-page__list {
	max-height: 350px;

	overflow-y: auto;
}
</style>
