<script setup>
import { inject } from "vue";

/**
 * Các state và hàm dùng chung
 * Author: LHH - 04/01/23
 */
const { state, handleCloseSidebar } = inject("store");
</script>

<template>
	<div class="header">
		<div class="header__right">
			<p
				v-if="state.sidebar.isOpen"
				class="header__icon"
				@click="handleCloseSidebar"
			>
				<i></i>
			</p>

			<h3 class="header__heading">
				công ty tnhh sản xuất - thương mại - dịch vụ qui phúc
				<i class="header__heading__icon"></i>
			</h3>
		</div>
		<div class="header__left">
			<p class="header__icon">
				<i></i>
			</p>
			<div class="header__user">
				<img
					src="../assets/images/default-avatar.jpg"
					alt=""
					class="header__user__avatar"
				/>
				<p class="header__user__name">
					lê huy hưng
					<i class="header__user__icon"></i>
				</p>
			</div>
		</div>
	</div>
</template>

<style></style>
