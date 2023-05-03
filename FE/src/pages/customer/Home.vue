<script setup>
import ProductSlider from "@/components/ProductSlider.vue";
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

const store = useStore();

const { useState, useActions, useGetters } = createNamespacedHelpers(
	store,
	"product"
);

const { product, pagination } = useState(["product", "pagination"]);

const { fetchProduct, remove, getProduct } = useActions([
	"fetchProduct",
	"remove",
	"getProduct",
]);

const newsStore = createNamespacedHelpers(store, "news");

const { news } = newsStore.useState(["news"]);

const { fetchNews } = newsStore.useActions(["fetchNews"]);

const initData = async () => {
	await fetchProduct();
	await fetchNews({ PageSize: 3, PageNumber: 1 });
};

/**
 * Call API
 * Author: LHH - 04/01/23
 */
onBeforeMount(() => {
	initData();

	if (store.state.user.user.Role == 0) {
		router.push("/admin");
	}
});
</script>

<template>
	<div class="top-slider">
		<img src="@/assets/images/slider_1.jpeg" alt="slider" />
	</div>
	<Container class="home px-md-5">
		<Row class="home__banner">
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_1.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_2.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_3.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col md="12" class="home__slider">
				<div class="home__link--wrap">
					<router-link to="" class="home__link">
						Sản phẩm
					</router-link>
				</div>

				<div
					class="d-none d-sm-none d-md-none d-lg-block overflow-hidden"
				>
					<ProductSlider :product="product" :slideToShow="4.5" />
				</div>
			</Col>
			<Col md="12" class="home__post">
				<div class="home__link--wrap">
					<router-link to="/news" class="home__link">
						Mẹo hay sử dụng
					</router-link>
				</div>
				<div class="home__post__list">
					<Row>
						<Col
							md="12"
							lg="4"
							v-for="(item, index) in news"
							:key="index"
						>
							<div class="home__post__item">
								<div class="home__post__item__img">
									<img :src="item.Thumbnail" alt="anh" />
								</div>
								<div class="home__post__item__content">
									<router-link
										:to="{ path: '/news/' + item.NewsId }"
										class="home__post__item__link"
									>
										{{ item.Title }}
									</router-link>
									<div
										class="product-card__name read-more"
										:style="{
											WebkitLineClamp: 4,
											display: '-webkit-box',
										}"
										v-html="item.Description"
									></div>
								</div>
							</div>
						</Col>
					</Row>
				</div>
			</Col>
		</Row>
	</Container>
</template>

<style></style>
