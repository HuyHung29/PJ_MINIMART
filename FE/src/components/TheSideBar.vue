<script setup>
import { inject, reactive } from "vue";
import RESOURCES from "../constants/resource";

/**
 * Các state và hàm dùng chung
 * Author: LHH - 04/01/23
 */
const { state, handleOpenSidebar } = inject("store");

const sidebarState = reactive({
	activeItem: 1,
});
</script>

<template>
	<div
		class="side-bar"
		:class="{
			'side-bar--close': !state.sidebar.isOpen,
		}"
	>
		<div class="side-bar__header">
			<i v-if="state.sidebar.isOpen" class="side-bar__header__icon"></i>
			<img
				v-if="state.sidebar.isOpen"
				class="side-bar__header__logo"
				src="../assets/images/logo.svg"
				alt=""
			/>
			<p
				v-if="!state.sidebar.isOpen"
				class="side-bar__header__open"
				@click="handleOpenSidebar"
			>
				<i></i>
			</p>
		</div>
		<ul class="side-bar__menu">
			<li
				class="side-bar__item"
				v-for="(item, index) in RESOURCES.SIDEBAR_NAME"
				:key="index"
				:class="{
					active: sidebarState.activeItem === index + 1,
				}"
				@click="sidebarState.activeItem = index + 1"
			>
				<p class="side-bar__item__icon">
					<i
						:class="{
							[`icon${index + 1}`]: true,
						}"
					></i>
				</p>
				<p class="side-bar__item__text">{{ item }}</p>
			</li>
		</ul>
	</div>
</template>

<style></style>
