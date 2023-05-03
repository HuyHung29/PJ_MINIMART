<script setup>
import { useStore } from "vuex";
import CartItem from "./CartItem.vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount, computed } from "vue";

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"cart"
);

const { fetchCart, remove } = useActions(["fetchCart", "remove"]);

const { cart } = useState(["cart"]);

const userStore = createNamespacedHelpers(store, "user");

const { isLogin } = userStore.useState(["isLogin"]);

const initData = async () => {
	await fetchCart();
};

onBeforeMount(() => {
	if (isLogin.value) {
		initData();
	}
});

const total = computed(() => {
	return cart.value
		? cart.value?.reduce((prev, curr) => {
				return prev + curr.CurrentPrice * curr.Quantity;
		  }, 0)
		: 0;
});

const handleDeleteItem = async (id) => {
	await remove([id]);
};
</script>

<template>
	<div class="cart">
		<div class="cart__group">
			<router-link to="/cart" class="cart__button">
				<i class="fas fa-shopping-bag"></i>
				<p>
					GIỎ HÀNG
					<span class="cart__quantity">({{ cart.length }})</span>
				</p>
			</router-link>

			<div class="cart__group__menu">
				<div class="cart--empty" v-if="cart.length == 0">
					<img src="@/assets/images/emptyCart.png" alt="empty cart" />
					<p>Chưa có sản phẩm</p>
				</div>

				<ul class="cart__list" v-if="cart && cart.length > 0">
					<CartItem
						v-for="(item, index) in cart"
						:key="index"
						:product="item"
						@delete="handleDeleteItem"
					/>
				</ul>
				<div class="cart__bottom" v-if="cart && cart.length > 0">
					<div class="cart__total-price">
						<p>Tổng tiền</p>
						<span>
							{{ total.toLocaleString() }}
							<small>đ</small>
						</span>
					</div>

					<router-link to="/purchase" class="flex-grow-1">
						<button class="btn buy-btn shadow-none">
							Tiến hành đặt hàng
						</button>
					</router-link>
				</div>
			</div>
		</div>
	</div>
</template>

<style></style>
