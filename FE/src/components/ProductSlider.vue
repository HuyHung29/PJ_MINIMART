<script setup>
import "vue3-carousel/dist/carousel.css";
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";
import ProductCart from "./ProductCart.vue";
</script>

<template>
	<div className="product-slider">
		<div className="product-slider__list">
			<carousel :items-to-show="4.5">
				<slide v-for="slide in 10" :key="slide">
					<ProductCart />
				</slide>

				<template #addons>
					<navigation />
				</template>
			</carousel>
		</div>
	</div>
</template>

<style scoped>
.home__slider .product-card--wrap {
	width: 100%;
	height: 100%;
}
</style>
