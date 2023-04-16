<script setup>
import { computed } from "@vue/reactivity";

const props = defineProps({
	class: {
		type: String,
		default: "",
	},
	fluid: {
		type: Boolean,
		default: false,
	},
});

const propClass = computed(() => {
	let classes = "";
	if (props.fluid) {
		classes += ` container-fluid `;
	}

	if (props.class) {
		classes += ` ${props.class} `;
	}

	return classes;
});
</script>

<template>
	<div class="container" :class="propClass">
		<slot />
	</div>
</template>

<style></style>
