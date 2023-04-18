<script setup>
import { useStore } from "vuex";
import CartItem from "./CartItem.vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"cart"
);

const { cart } = useState(["cart"]);
</script>

<template>
	<div className="cart">
		<div className="cart__group">
			<router-link to="/cart" className="cart__button">
				<i className="fas fa-shopping-bag"></i>
				<p>
					GIỎ HÀNG
					<span className="cart__quantity">(0)</span>
				</p>
			</router-link>

			<div className="cart__group__menu">
				<div className="cart--empty" v-if="cart.length == 0">
					<img src="@/assets/images/emptyCart.png" alt="empty cart" />
					<p>Chưa có sản phẩm</p>
				</div>

				<ul className="cart__list" v-else>
					<CartItem :product="{}" />
				</ul>
				<div className="cart__bottom" v-else>
					<div className="cart__total-price">
						<p>Tổng tiền</p>
						<span>
							1.000.000
							<small>đ</small>
						</span>
					</div>

					<router-link to="/purchase" className="flex-grow-1">
						<button className="btn buy-btn shadow-none">
							Tiến hành đặt hàng
						</button>
					</router-link>
				</div>
			</div>
		</div>
	</div>
</template>

<style></style>
