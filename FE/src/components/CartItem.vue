<script setup>
const props = defineProps({
	product: {
		type: Object,
		required: true,
	},
});
</script>

<template>
	<li className="cart__item">
		<router-link to="/" className="cart__item__img">
			<img src="{pictures[0]}" alt="anh" />
		</router-link>
		<div className="cart__item__info">
			<div className="cart__item__info--wrap">
				<h3 className="cart__item__name">
					<router-link to="/"> Tên sản phẩm </router-link>
				</h3>
				<p className="cart__item__price">100.000 <small>đ/kg</small></p>
				<p className="cart__item__quantity">Số lượng: 10</p>
			</div>
			<div className="cart__item__action" @click="">
				<i className="fas fa-trash-alt"></i>
			</div>
		</div>
	</li>
</template>

<style></style>
