<script setup>
import { computed } from "@vue/reactivity";

const props = defineProps({
	class: {
		type: String,
		default: "",
	},
});

const propClass = computed(() => {
	let classes = "";

	if (props.class) {
		classes += ` ${props.class} `;
	}

	return classes;
});
</script>

<template>
	<div class="row" :class="propClass">
		<slot />
	</div>
</template>

<style></style>
