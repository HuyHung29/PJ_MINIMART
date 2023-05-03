<script setup>
import { useRouter } from "vue-router";
import Col from "./bootstrap/Col.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { computed } from "vue";

const store = useStore();

const { useActions } = createNamespacedHelpers(store, "product");

const { getProduct } = useActions(["fetchProduct", "remove", "getProduct"]);

const props = defineProps({
	product: Object,
	width: String,
});

const router = useRouter();

const handleClick = async () => {
	await getProduct(props.product.ProductId);
	router.push("/product/" + props.product.ProductId);

	window.scrollTo(0, 0);
};

const cartStore = createNamespacedHelpers(store, "cart");

const { cart } = cartStore.useState(["cart"]);

const { insert } = cartStore.useActions(["insert"]);

const isInCart = computed(() => {
	if (cart.value && props.product) {
		return (
			cart.value.findIndex(
				(item) => item.ProductId === props.product.ProductId
			) >= 0
		);
	}
});

const handleAddToCart = async () => {
	if (store.state.user.isLogin) {
		const newPro = {
			...props.product,
			Quantity: 1,
			MainQuantity: props.product.Quantity,
		};
		await insert(newPro);
	} else {
		router.push("/login");
	}
};
</script>

<template>
	<Col
		xs="6"
		sm="6"
		md="4"
		:lg="width !== '2-4' ? width : ''"
		class="product-card--wrap"
		:class="{
			'col-2-4': width === '2-4',
		}"
	>
		<div class="product-card">
			<div class="product-card__link">
				<div class="product-card__img" @click="handleClick">
					<img :src="product.Pictures[0].Url" alt="anh" />
				</div>
				<div class="product-card__info">
					<div
						class="product-card__name read-more"
						style="{WebkitLineClamp: 1,
		                        display: isReadMore ? '-webkit-box' : 'block'}"
					>
						{{ product.ProductName }}
						<span v-if="product.Quantity === 0" class="out-of-stock"
							>(Hết hàng)</span
						>
					</div>

					<div class="product-card__price--wrap">
						<p class="product__price" v-if="product.Disscount == 0">
							{{ product.CurrentPrice.toLocaleString() }}
							<sup>đ</sup>/kg
						</p>
						<p class="product__price--old" v-else>
							{{ product.Price.toLocaleString() }} <sup>đ</sup>/kg
						</p>

						<p class="product__price" v-else>
							{{ product.CurrentPrice.toLocaleString() }}
							<sup>đ</sup>/kg
						</p>
					</div>
				</div>
			</div>
			<p class="product-card__view--btn" v-if="false">
				<i class="fas fa-eye"></i>
			</p>
			<p class="product-card__favorite--btn">
				<i v-if="false" class="fas fa-heart"></i>

				<i v-if="false" class="far fa-heart"></i>
			</p>
			<button
				class="btn add-cart-btn shadow-none"
				v-if="!isInCart && product.Quantity > 0"
				@click="handleAddToCart"
			>
				Thêm vào giỏ hàng
			</button>
			<button
				class="add-cart-btn add-cart-btn--done shadow-none"
				v-if="isInCart"
			>
				Đã có trong giỏ hàng
			</button>
		</div>
	</Col>
</template>

<style>
.product-card:hover .add-cart-btn {
	display: block;
}

.add-cart-btn {
	display: none;
	position: absolute;

	top: 50%;
	left: 10px;
	right: -10px;
	color: white;
}

.out-of-stock {
	color: red;
}
</style>
