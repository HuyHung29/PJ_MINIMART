<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import ProductCard from "@/components/ProductCard.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount, watch, computed } from "vue";
import { useRoute } from "vue-router";
import Pagination from "@/components/Pagination.vue";
import SubMenu from "@/components/SubMenu.vue";
import MRadio from "@/components/customs/MRadio.vue";

const route = useRoute();

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"product"
);

const cateStore = createNamespacedHelpers(store, "category");

const { category } = cateStore.useState(["category"]);

const { product, pagination } = useState(["product", "pagination"]);

const { fetchProduct, remove, getProduct } = useActions([
	"fetchProduct",
	"remove",
	"getProduct",
]);

const initData = async () => {
	await fetchProduct({ ...route.query });
};

const title = computed(() => {
	if (route.query.CategoryId) {
		const find = category.value.find(
			(item) => item.CategoryId === route.query.CategoryId
		);

		return find.CategoryName;
	} else {
		return "Danh sách sản phẩm";
	}
});

onBeforeMount(() => {
	initData();

	// if (route.query.CategoryId) {
	// 	fetchProduct({ CategoryId: route.query.CategoryId, PageNumber: 1 });
	// } else if (route.query.Filter) {
	// 	fetchProduct({
	// 		Filter: route.query.Filter,
	// 		PageNumber: 1,
	// 	});
	// } else {
	// 	initData();
	// }

	// console.log(route.query);
});

watch(
	() => route.query,
	(value) => {
		// if (route.query.CategoryId) {
		// 	fetchProduct({
		// 		CategoryId: route.query.CategoryId,
		// 		PageNumber: 1,
		// 		...value,
		// 	});
		// } else if (route.query.Filter) {
		// 	fetchProduct({
		// 		Filter: route.query.Filter,
		// 		PageNumber: 1,
		// 		...value,
		// 	});
		// } else {
		// }
		fetchProduct({
			...value,
		});
	}
);
</script>

<template>
	<Container class="product-list--wrap px-md-5">
		<Row>
			<Col lg="2" class="d-none d-lg-block">
				<h3 class="product-list__title">Danh mục sản phẩm</h3>
				<SubMenu />
				<h3 class="product-list__title">Tìm theo giá</h3>
				<ul className="sort-price">
					<li className="sort-price__item">
						<router-link
							to="/product?Price=100000"
							:class="{ active: route.query.Price == 100000 }"
						>
							Giá dưới 100.000<sup>đ</sup>
						</router-link>
					</li>
					<li className="sort-price__item">
						<router-link
							to="/product?Price=300000"
							:class="{ active: route.query.Price == 300000 }"
						>
							Giá dưới 300.000<sup>đ</sup>
						</router-link>
					</li>
					<li className="sort-price__item">
						<router-link
							to="/product?Price=500000"
							:class="{ active: route.query.Price == 500000 }"
						>
							Giá dưới 500.000<sup>đ</sup>
						</router-link>
					</li>
					<li className="sort-price__item">
						<router-link
							to="/product?Price=1000000"
							:class="{ active: route.query.Price == 1000000 }"
						>
							Giá dưới 1.000.000<sup>đ</sup>
						</router-link>
					</li>
					<li className="sort-price__item">
						<router-link
							to="/product?Price=1500000"
							:class="{ active: route.query.Price == 1500000 }"
						>
							Giá dưới 1.500.000<sup>đ</sup>
						</router-link>
					</li>
					<li className="sort-price__item">
						<router-link
							to="/product?Price=2000000"
							:class="{ active: route.query.Price == 2000000 }"
						>
							Giá dưới 2.000.000<sup>đ</sup>
						</router-link>
					</li>
				</ul>
			</Col>
			<Col>
				<h3 class="product-list__title">{{ title }}</h3>
				<Row class="product-list">
					<div v-if="product.length == 0" class="list-empty">
						<img src="@/assets/images/list-empty.png" alt="anh" />
						<p>Chưa có sản phẩm nào</p>
					</div>
					<ProductCard
						v-for="item in product"
						:key="item.ProductId"
						:product="item"
						width="3"
					/>
				</Row>
				<div class="product--footer">
					<Pagination v-if="pagination" :pagination="pagination" />
				</div>
			</Col>
		</Row>
	</Container>
</template>

<style scoped>
.product--footer {
	background-color: white;

	padding: 6px 10px;

	border-radius: 5px;
}

.data-table__footer {
	margin-top: 0;
}

.sort-price {
	background-color: white;

	border-radius: 5px;
}

.sort-price__item {
	margin: 0;
	vertical-align: unset;
	cursor: pointer;
}

.sort-price__item a {
	display: block;
	font-size: 14px;

	padding: 10px;
}
.sort-price__item a:hover {
	background-color: #eee;
}

.sort-price__item a.active {
	background-color: #eee;
	color: orange;
}
</style>
