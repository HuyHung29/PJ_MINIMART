<script setup>
import RESOURCES from "../constants/resource";
import { inject, onMounted, onUpdated, ref } from "vue";

const { handleCloseToast } = inject("store");
/**
 * Định nghĩa các props
 * Author: LHH - 09/01/23
 */
const props = defineProps({
	toast: {
		type: Object,
	},
});

/**
 * Khai báo state của component
 * Author: LHH - 12/01/23
 */
const isShow = ref(true);

let removeToast = null;

/**
 * Xử lý đóng toast
 * Author: LHH - 12/01/23
 */
onMounted(() => {
	removeToast = setTimeout(function () {
		isShow.value = false;
	}, 4300);
});

/**
 * Xử lý khi nhấn vào dẫu x
 * Author: LHH -12/01/23
 */
const onClickCloseBtn = () => {
	isShow.value = false;
	clearTimeout(removeToast);
};
</script>

<template>
	<div
		v-if="isShow"
		class="toast"
		:class="{
			'toast--success':
				toast.type === RESOURCES.NOTIFICATION_TYPE.SUCCESS,
			'toast--error': toast.type === RESOURCES.NOTIFICATION_TYPE.ERROR,
			'toast--info': toast.type === RESOURCES.NOTIFICATION_TYPE.INFO,
			'toast--warning':
				toast.type === RESOURCES.NOTIFICATION_TYPE.WARNING,
		}"
	>
		<p class="toast__icon">
			<i class="toast__icon__img"></i>
		</p>
		<div class="toast__content">
			<p class="toast__content__title">
				{{ RESOURCES.NOTIFICATION_TITLE[toast.type] }}!
			</p>
			<p class="toast__content__text">{{ toast.content }}</p>
		</div>
		<div class="toast__action">
			<a class="toast__action__main" v-if="toast.action">Hoàn tác</a>
			<p class="toast__action_close" @click="onClickCloseBtn">
				<i class="toast__action_close__img"></i>
			</p>
		</div>
	</div>
</template>

<style></style>
