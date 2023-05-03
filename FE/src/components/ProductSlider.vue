<script setup>
import "vue3-carousel/dist/carousel.css";
import { Carousel, Slide, Pagination, Navigation } from "vue3-carousel";
import ProductCard from "./ProductCard.vue";

const props = defineProps({
	product: {
		type: Array,
		default: [],
	},
	slideToShow: {
		type: Number,
		default: 4,
	},
});
</script>

<template>
	<div class="product-slider">
		<div class="product-slider__list">
			<carousel :items-to-show="slideToShow">
				<slide v-for="slide in product" :key="slide">
					<ProductCard :product="slide" />
				</slide>

				<template #addons>
					<navigation />
				</template>
			</carousel>
		</div>
	</div>
</template>

<style scoped>
.product-card--wrap {
	width: 100% !important;
	height: 100% !important;
}
</style>
