<script setup>
import { computed } from "@vue/reactivity";

const props = defineProps({
	xs: String,
	sm: String,
	md: String,
	lg: String,
	class: String,
});

const propClass = computed(() => {
	let classes = "";
	if (props.xs) {
		classes += ` col-xs-${props.xs} `;
	}

	if (props.sm) {
		classes += ` col-sm-${props.sm} `;
	}

	if (props.md) {
		classes += ` col-md-${props.md} `;
	}

	if (props.lg) {
		classes += ` col-lg-${props.lg} `;
	}

	if (props.class) {
		classes += ` ${props.class} `;
	}

	return classes;
});
</script>

<template>
	<div class="col" :class="propClass">
		<slot />
	</div>
</template>

<style></style>
