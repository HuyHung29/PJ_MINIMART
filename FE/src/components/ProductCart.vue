<script setup>
import Col from "./bootstrap/Col.vue";

const props = defineProps({
	product: Object,
	width: String,
});
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
			<router-link to="" class="product-card__link">
				<div class="product-card__img">
					<img
						src="https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/78662b27204075.563614c05abc6.png"
						alt="anh"
					/>
				</div>
				<div class="product-card__info">
					<div
						class="product-card__name read-more"
						style="{WebkitLineClamp: 1,
		                        display: isReadMore ? '-webkit-box' : 'block'}"
					>
						Hoa qua ngon
					</div>

					<div class="product-card__price--wrap">
						<p class="product__price" v-if="true">
							100000 <sup>đ</sup>/kg
						</p>
						<p class="product__price--old" v-if="true">
							10000 <sup>đ</sup>/kg
						</p>

						<p class="product__price" v-else>
							20000 <sup>đ</sup>/kg
						</p>
					</div>
				</div>
			</router-link>
			<p class="product-card__view--btn">
				<i class="fas fa-eye"></i>
			</p>
			<p class="product-card__favorite--btn">
				<i v-if="true" class="fas fa-heart"></i>

				<i v-else class="far fa-heart"></i>
			</p>
			<button class="btn add-cart-btn shadow-none" v-if="true">
				Thêm vào giỏ hàng
			</button>
			<button class="add-cart-btn add-cart-btn--done shadow-none" v-else>
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
}
</style>
