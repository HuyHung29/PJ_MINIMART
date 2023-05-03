<script setup>
import SubMenu from "@/components/SubMenu.vue";
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import moment from "moment";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "news");

const { currentItem } = useState(["currentItem"]);

const { getNews } = useActions(["getNews"]);

onBeforeMount(() => {
	getNews(route.params.id);
	window.scrollTo(0, 0);
});
</script>

<template>
	<Container class="px-md-5" v-if="currentItem">
		<Row class="post__detail">
			<Col lg="2" class="d-md-none">
				<h2 class="post__detail__title">Danh mục tin tức</h2>
				<SubMenu />
			</Col>
			<Col>
				<h2 class="post__detail__title">Chi tiết bài viết</h2>
				<div class="post__detail__content">
					<h3 class="post__detail__content__title">
						{{ currentItem.Title }}
					</h3>
					<p class="post__detail__content__time">
						{{
							moment(currentItem.CreatedDate).format("DD/MM/YYYY")
						}}
					</p>
					<div
						class="post__detail__content__des"
						v-html="currentItem.Description"
					></div>

					<p class="post__detail__share">Chia sẻ:</p>
					<div
						class="product-detail__page__info__share__list post__detail__share--wrap"
					>
						<p class="product-detail__page__info__share__item">
							<i class="fab fa-facebook-messenger"></i>
						</p>
						<p class="product-detail__page__info__share__item">
							<i class="fab fa-twitter"></i>
						</p>
						<p class="product-detail__page__info__share__item">
							<i class="fab fa-facebook-f"></i>
						</p>
						<p class="product-detail__page__info__share__item">
							<i class="fab fa-pinterest"></i>
						</p>
					</div>
				</div>
			</Col>
		</Row>
	</Container>
</template>

<style scoped>
.post__detail__title {
	font-size: 20px;
	font-weight: bold;
}
</style>
