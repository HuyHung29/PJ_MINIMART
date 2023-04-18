<script setup>
import Footer from "@/components/Footer.vue";
import Navbar from "@/components/Navbar.vue";
import Header from "@/components/customer/Header.vue";
</script>

<template>
	<div className="layout__content__background">
		<Header />
		<Navbar />
		<router-view></router-view>
		<Footer />
	</div>
</template>

<style></style>
