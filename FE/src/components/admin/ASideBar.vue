<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

const store = useStore();

const uiStore = createNamespacedHelpers(store, "ui");
const { isOpenSidebar } = uiStore.useState(["isOpenSidebar"]);
const { openSidebar } = uiStore.useMutations(["openSidebar"]);
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
			<img
				v-if="isOpenSidebar"
				class="a-side-bar__header__logo"
				src="@/assets/images/logo.png"
				alt=""
			/>
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
					active: true,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon1"></i>
				</p>
				<p class="a-side-bar__item__text">Tổng quan</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon2"></i>
				</p>
				<p class="a-side-bar__item__text">Danh mục</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon3"></i>
				</p>
				<p class="a-side-bar__item__text">Nhà cung cấp</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon4"></i>
				</p>
				<p class="a-side-bar__item__text">Sản phẩm</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon5"></i>
				</p>
				<p class="a-side-bar__item__text">Hóa đơn</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon6"></i>
				</p>
				<p class="a-side-bar__item__text">Đơn hàng</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon8"></i>
				</p>
				<p class="a-side-bar__item__text">Tài khoản</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon9"></i>
				</p>
				<p class="a-side-bar__item__text">Tin tức</p>
			</li>
			<li
				class="a-side-bar__item"
				:class="{
					active: false,
				}"
			>
				<p class="a-side-bar__item__icon">
					<i class="icon11"></i>
				</p>
				<p class="a-side-bar__item__text">Giảm giá</p>
			</li>
		</ul>
	</div>
</template>

<style></style>
