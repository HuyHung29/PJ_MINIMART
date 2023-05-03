<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";
import { useRoute } from "vue-router";

const store = useStore();

const uiStore = createNamespacedHelpers(store, "ui");
const { isOpenSidebar } = uiStore.useState(["isOpenSidebar"]);
const { openSidebar } = uiStore.useMutations(["openSidebar"]);

const route = useRoute();
</script>

<template>
	<div
		class="a-side-bar"
		:class="{
			'a-side-bar--close': !isOpenSidebar,
		}"
	>
		<div class="a-side-bar__header">
			<i v-if="isOpenSidebar" class="a-side-bar__header__icon"></i>
			<router-link to="/admin">
				<img
					v-if="isOpenSidebar"
					class="a-side-bar__header__logo"
					src="@/assets/images/logo.png"
					alt=""
				/>
			</router-link>
			<p
				v-if="!isOpenSidebar"
				class="a-side-bar__header__open"
				@click="openSidebar"
			>
				<i></i>
			</p>
		</div>
		<ul class="a-side-bar__menu">
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path == '/admin',
				}"
			>
				<router-link to="/admin" class="a-side-bar__item__icon">
					<i class="icon1"></i>
				</router-link>
				<router-link to="/admin" class="a-side-bar__item__text"
					>Tổng quan</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('category'),
				}"
			>
				<router-link
					to="/admin/category"
					class="a-side-bar__item__icon"
				>
					<i class="icon2"></i>
				</router-link>
				<router-link to="/admin/category" class="a-side-bar__item__text"
					>Danh mục</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('supplier'),
				}"
			>
				<router-link
					to="/admin/supplier"
					class="a-side-bar__item__icon"
				>
					<i class="icon3"></i>
				</router-link>
				<router-link to="/admin/supplier" class="a-side-bar__item__text"
					>Nhà cung cấp</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('product'),
				}"
			>
				<router-link to="/admin/product" class="a-side-bar__item__icon">
					<i class="icon4"></i>
				</router-link>
				<router-link to="/admin/product" class="a-side-bar__item__text"
					>Sản phẩm</router-link
				>
			</li>
			<li
				v-if="false"
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('invoice'),
				}"
			>
				<router-link to="/admin/invoice" class="a-side-bar__item__icon">
					<i class="icon5"></i>
				</router-link>
				<router-link to="/admin/invoice" class="a-side-bar__item__text"
					>Hóa đơn</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('order'),
				}"
			>
				<router-link to="/admin/order" class="a-side-bar__item__icon">
					<i class="icon6"></i>
				</router-link>
				<router-link to="/admin/order" class="a-side-bar__item__text"
					>Đơn hàng</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('account'),
				}"
			>
				<router-link to="/admin/account" class="a-side-bar__item__icon">
					<i class="icon8"></i>
				</router-link>
				<router-link to="/admin/account" class="a-side-bar__item__text"
					>Tài khoản</router-link
				>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('news'),
				}"
			>
				<router-link to="/admin/news" class="a-side-bar__item__icon">
					<i class="icon9"></i>
				</router-link>
				<router-link to="/admin/news" class="a-side-bar__item__text"
					>Tin tức</router-link
				>
			</li>
			<li
				v-if="false"
				class="a-side-bar__item"
				:class="{
					active: route.path.includes('coupon'),
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon11"></i>
				</p>
				<router-link to="/admin/coupon" class="a-side-bar__item__text"
					>Giảm giá</router-link
				>
			</li>
		</ul>
	</div>
</template>

<style scoped></style>
