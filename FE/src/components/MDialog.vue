<script setup>
import RESOURCES from "../constants/resource";
import { inject, ref, onMounted, onUnmounted } from "vue";
import useEmployee from "../composable/employee";
import Button from "./MButton.vue";

/**
 * Các state và phương thức dùng chung
 * Author: LHH - 04/01/23
 */
const { state, handleCloseModal, handleCloseForm } = inject("store");

/**
 * Các state và phương thức liên quan đến employee
 * Author: LHH - 04/01/23
 */
const { statusCode, deleteEmployee } = useEmployee();

const closeBtnRef = ref(null);

/**
 * Xử lý đóng dialog
 * Author: LHH - 04/01/23
 */
const handleCloseAll = () => {
	try {
		handleCloseModal();
		handleCloseForm();
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý ấn nút xóa
 * Author: LHH - 04/01/23
 */
const handleAgreeBtnClick = async () => {
	try {
		await state.modal.callback();
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
		state.modal.callback();
		handleCloseModal();
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý phím tắt trong modal
 * @param {event} e Sự kiện html
 */
function docKeyDown(e) {
	if (e.code === "Escape") {
		handleCloseModal();
	}
}

/**
 * Xử lý focus vào mã nhân viên và các phím tắt ở form
 * Author: LHH - 27/01/23
 */
onMounted(() => {
	if (closeBtnRef.value) {
		closeBtnRef.value.setFocusBtn();
	}
	document.addEventListener("keydown", docKeyDown, false);
});

/**
 * Hàm xử lý remove event
 * Author: LHH - 23/03/23
 */
onUnmounted(() => {
	document.removeEventListener("keydown", docKeyDown);
});
</script>

<template>
	<div class="overlay">
		<div
			class="dialog"
			:class="{
				'dialog--error':
					state.modal.type === RESOURCES.MODAL_TYPE.ERROR,
				'dialog--info': state.modal.type === RESOURCES.MODAL_TYPE.INFO,
			}"
		>
			<div class="dialog__header">
				<h3 class="dialog__heading">{{ state.modal.title }}</h3>
				<p class="dialog__close-btn" @click="handleCloseModal">
					<i></i>
				</p>
			</div>
			<div class="dialog__body">
				<div class="dialog__content">
					<p class="dialog__content__icon"></p>
					<p class="dialog__content__text">
						{{ state.modal.content }}
					</p>
				</div>
			</div>
			<div class="dialog__action">
				<Button
					type="sub"
					@click="handleCloseModal"
					v-if="state.modal.type === RESOURCES.MODAL_TYPE.INFO"
					content="Hủy"
				/>
				<Button
					type="sub"
					@click="handleCloseAll"
					v-if="state.modal.type !== RESOURCES.MODAL_TYPE.ERROR"
					content="Không"
				/>
				<Button
					@click="handleAgreeBtnClick"
					v-if="state.modal.type !== RESOURCES.MODAL_TYPE.ERROR"
					content="Có"
					:type="
						state.modal.type === RESOURCES.MODAL_TYPE.WARNING
							? 'warning'
							: 'primary'
					"
				/>
				<Button
					@click="handleCloseBtnClick"
					v-if="state.modal.type === RESOURCES.MODAL_TYPE.ERROR"
					content="Đóng"
					ref="closeBtnRef"
				/>
			</div>
		</div>
	</div>
</template>

<style></style>
