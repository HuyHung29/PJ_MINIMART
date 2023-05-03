<script setup>
import Footer from "@/components/Footer.vue";
import Navbar from "@/components/Navbar.vue";
import ScrollTopBtn from "@/components/ScrollTopBtn.vue";
import Header from "@/components/customer/Header.vue";
</script>

<template>
	<div class="layout__content__background">
		<Header />
		<Navbar />
		<router-view></router-view>
		<Footer />
		<ScrollTopBtn />
	</div>
</template>

<style></style>
