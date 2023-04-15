<script setup>
import { ref } from "vue";

/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	type: {
		type: String,
		validator(value) {
			return ["primary", "sub", "warning"].includes(value);
		},
		default: "primary",
	},
	content: {
		type: String,
		required: true,
	},
	action: {
		type: String,
		validator(value) {
			return ["submit", "reset", "button"].includes(value);
		},
		default: "button",
	},
	tooltip: String,
	style: Object,
});

const btnRef = ref(null);

const setFocusBtn = () => {
	btnRef.value.focus();
};

defineExpose({ setFocusBtn });
</script>

<template>
	<button
		ref="btnRef"
		:type="action"
		class="btn"
		:class="{
			'btn--sub': type === 'sub',
			'btn--warning': type === 'warning',
		}"
		:style="style"
	>
		<span class="btn__text">{{ content }}</span>
		<p v-if="tooltip" class="btn__tooltip">{{ tooltip }}</p>
	</button>
</template>

<style></style>
