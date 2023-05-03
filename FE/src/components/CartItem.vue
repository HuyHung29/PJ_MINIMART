<script setup>
const props = defineProps({
	product: {
		type: Object,
		required: true,
	},
});

const emits = defineEmits(["delete"]);

const handleDeleteItem = () => {
	emits("delete", props.product.ProductId);
};
</script>

<template>
	<li class="cart__item">
		<router-link
			:to="{ path: '/product/' + product.ProductId }"
			class="cart__item__img"
		>
			<img :src="product.Pictures[0].Url" alt="anh" />
		</router-link>
		<div class="cart__item__info">
			<div class="cart__item__info--wrap">
				<h3 class="cart__item__name">
					<router-link
						:to="{ path: '/product/' + product.ProductId }"
					>
						{{ product.ProductName }}
						<span
							class="out-of-stock"
							v-if="product.Quantity > product.MainQuantity"
							>(Hết hàng)</span
						>
					</router-link>
				</h3>
				<p class="cart__item__price">
					{{ product.CurrentPrice.toLocaleString() }}
					<small>đ/{{ product.UnitName }}</small>
				</p>
				<p class="cart__item__quantity">
					Số lượng: {{ product.Quantity }}
				</p>
			</div>
			<div class="cart__item__action" @click="handleDeleteItem">
				<i class="fas fa-trash-alt"></i>
			</div>
		</div>
	</li>
</template>

<style scoped>
.out-of-stock {
	color: red;
}
</style>
