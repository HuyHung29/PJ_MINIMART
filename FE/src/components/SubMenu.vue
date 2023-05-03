<script setup>
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";

const store = useStore();

const router = useRouter();

const route = useRoute();

const { useState, useActions } = createNamespacedHelpers(store, "category");

const { category } = useState(["category"]);

const { fetchCategory } = useActions(["fetchCategory"]);

const initData = async () => {
	await fetchCategory();
};

onBeforeMount(() => {
	initData();
});

const handleClick = (id) => {
	router.push("/product?CategoryId=" + id);
};

const state = reactive({
	isShow: false,
});
</script>

<template>
	<div class="sub-menu">
		<ul class="sub-menu__list">
			<li class="sub-menu__item">
				<a class="sub-menu__link" to="/"> Trang chủ </a>
			</li>
			<li class="sub-menu__item">
				<router-link
					to="/"
					class="sub-menu__link"
					activeClassName="active"
				>
					Giới thiệu
				</router-link>
			</li>
			<li class="sub-menu__item">
				<p
					class="sub-menu__link sub-menu__link--not"
					@click="state.isShow = !state.isShow"
				>
					Sản phẩm
					<i
						class="fas fa-chevron-right sub-menu__icon"
						:class="{ active: state.isShow }"
					></i>
				</p>
				<ul class="second-menu" :class="{ active: state.isShow }">
					<li
						class="second-menu__item"
						v-for="(item, index) in category"
						:key="index"
					>
						<p
							class="sub-menu__link"
							:class="{
								active:
									route.query.CategoryId == item.CategoryId,
							}"
							@click="() => handleClick(item.CategoryId)"
						>
							{{ item.CategoryName }}
						</p>
					</li>
				</ul>
			</li>
			<li class="sub-menu__item">
				<router-link to="/news" class="sub-menu__link">
					Tin tức
				</router-link>
			</li>
		</ul>
	</div>
</template>

<style></style>
