<script setup>
import { onMounted, onBeforeUnmount, ref } from "vue";

const visible = ref(false);

const toggleVisible = () => {
	const scrolled = document.documentElement.scrollTop;
	if (scrolled > 300) {
		visible.value = true;
	} else if (scrolled <= 300) {
		visible.value = false;
	}
};

onMounted(() => {
	window.addEventListener("scroll", toggleVisible);
});

onBeforeUnmount(() => {
	window.removeEventListener("scroll", toggleVisible);
});

const scrollToTop = () => {
	window.scrollTo({
		top: 0,
		behavior: "smooth",
	});
};
</script>

<template>
	<div
		className="scroll-btn shadow-sm"
		@click="scrollToTop"
		:style="{ display: visible ? 'flex' : 'none' }"
	>
		<i className="fa-solid fa-chevron-up"></i>
	</div>
</template>

<style></style>
