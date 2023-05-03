<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import { useStore } from "vuex";
import { onBeforeMount } from "vue";
import moment from "moment";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import SubMenu from "@/components/SubMenu.vue";
import { useRouter } from "vue-router";

const router = useRouter();
const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "news");

const { news } = useState(["news"]);

const { fetchNews, getNews } = useActions(["fetchNews", "getNews"]);

const initData = async () => {
	await fetchNews({ PageSize: 20, PageNumber: 1 });
};

onBeforeMount(() => {
	initData();
});

const handleClick = async (id) => {
	await getNews(id);
	router.push("/news/" + id);
};
</script>

<template>
	<Container class="px-md-5">
		<Row class="post-list">
			<Col lg="2">
				<h2 class="post-list__title">Danh mục tin tức</h2>
				<SubMenu />
			</Col>
			<Col>
				<h2 class="post-list__title">Tin tức</h2>
				<Row>
					<Col
						md="6"
						class="post-list__item--wrap"
						v-for="(item, index) in news"
						:key="index"
					>
						<div class="post-list__item">
							<a @click="() => handleClick(item.NewsId)">
								<img :src="item.Thumbnail" alt="anh" />
							</a>
							<div class="post-list__item__content">
								<a
									@click="() => handleClick(item.NewsId)"
									class="post-list__item__title"
								>
									{{ item.Title }}
								</a>
								<p class="post-list__item__time">
									{{
										moment(item.CreatedDate).format(
											"DD/MM/YYYY"
										)
									}}
								</p>
								<div
									class="product-card__name read-more"
									:style="{
										WebkitLineClamp: 5,
										display: '-webkit-box',
									}"
									v-html="item.Description"
								></div>
							</div>
						</div>
					</Col>
				</Row>
			</Col>
		</Row>
	</Container>
</template>

<style></style>
