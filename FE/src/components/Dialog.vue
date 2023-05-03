<script setup>
import RESOURCES from "@/constants/resource";
import Button from "./MButton.vue";

import { createNamespacedHelpers } from "vuex-composition-helpers";
import { useStore } from "vuex";

const store = useStore();

const { useState, useMutations } = createNamespacedHelpers(store, "ui");
const { modal } = useState(["modal"]);
const { handleCloseModal } = useMutations(["handleCloseModal"]);

/**
 * Xử lý đóng dialog
 * Author: LHH - 04/01/23
 */
const handleCloseAll = () => {
	handleCloseModal();
};

/**
 * Xử lý ấn nút xóa
 * Author: LHH - 04/01/23
 */
const handleAgreeBtnClick = async () => {
	try {
		await modal.value.callback(modal.value.data);
		handleCloseModal();
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý ấn nút đóng
 * Author: LHH - 04/01/23
 */
const handleCloseBtnClick = () => {
	try {
		modal.value.callback();
		handleCloseModal();
	} catch (error) {
		console.log(error);
	}
};
</script>

<template>
	<div class="overlay">
		<div
			class="dialog"
			:class="{
				'dialog--error': modal.type === RESOURCES.MODAL_TYPE.ERROR,
				'dialog--info': modal.type === RESOURCES.MODAL_TYPE.INFO,
			}"
		>
			<div class="dialog__header">
				<h3 class="dialog__heading">{{ modal.title }}</h3>
				<p class="dialog__close-btn" @click="handleCloseModal">
					<i></i>
				</p>
			</div>
			<div class="dialog__body">
				<div class="dialog__content">
					<p class="dialog__content__icon"></p>
					<p class="dialog__content__text">
						{{ modal.content }}
					</p>
				</div>
			</div>
			<div class="dialog__action">
				<Button
					type="sub"
					@click="handleCloseModal"
					v-if="modal.type === RESOURCES.MODAL_TYPE.INFO"
					content="Hủy"
				/>
				<Button
					type="sub"
					@click="handleCloseAll"
					v-if="modal.type !== RESOURCES.MODAL_TYPE.ERROR"
					content="Không"
				/>
				<Button
					@click="handleAgreeBtnClick"
					v-if="modal.type !== RESOURCES.MODAL_TYPE.ERROR"
					content="Có"
					:type="
						modal.type === RESOURCES.MODAL_TYPE.WARNING
							? 'warning'
							: 'primary'
					"
				/>
				<Button
					@click="handleCloseBtnClick"
					v-if="modal.type === RESOURCES.MODAL_TYPE.ERROR"
					content="Đóng"
				/>
			</div>
		</div>
	</div>
</template>

<style></style>
