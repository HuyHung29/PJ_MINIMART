<script setup>
import { onBeforeMount } from "vue";
import { useRouter } from "vue-router";
import { createNamespacedHelpers, useStore } from "vuex-composition-helpers";

const store = useStore();

const router = useRouter();

const { useState, useActions } = createNamespacedHelpers(store, "category");

const { category } = useState(["category"]);

const { fetchCategory } = useActions(["fetchCategory"]);

const initData = async () => {
	fetchCategory({
		PageSize: 1000,
		PageNumber: 1,
	});
};

onBeforeMount(() => {
	initData();
});

const handleRoute = (item) => {
	router.push("/product?CategoryId=" + item.CategoryId);
};
</script>

<template>
	<div class="navbar--wrap">
		<div class="p-md-0 container">
			<div class="align-items-center g-md-0 row">
				<div class="navabr--wrap col-md-12 col-lg-3">
					<nav class="py-0 navbar navbar-expand">
						<div class="container-fluid">
							<button
								aria-label="Toggle navigation"
								type="button"
								class="navbar-toggler"
							>
								<span class="navbar-toggler-icon"></span>
							</button>
							<div class="collapse navbar-collapse">
								<ul class="navbar-nav">
									<li class="dropdown nav-item">
										<router-link
											class="nav-link"
											to="/products"
											><i
												class="fas fa-bars nav__icon"
											></i
											>Danh mục</router-link
										>
										<div
											tabindex="-1"
											role="menu"
											aria-hidden="true"
											class="dropdown-menu dropdown-menu-end"
										>
											<li
												v-for="item in category"
												:key="item.CategoryId"
											>
												<p
													class="dropdown-item"
													@click="
														() => handleRoute(item)
													"
												>
													{{ item.CategoryName }}
												</p>
											</li>
										</div>
									</li>
								</ul>
								<div class="navbar__user d-lg-none d-block">
									<i class="fa-solid fa-user"></i>
									<ul class="navbar__user__nav shadow">
										<li class="navbar__user__item">
											<router-link
												class="navbar__user__link"
												to="/user/register"
												>Đăng ký</router-link
											>
										</li>
										<li class="navbar__user__item">
											<router-link
												class="navbar__user__link"
												to="/user/login"
												>Đăng nhập</router-link
											>
										</li>
									</ul>
								</div>
							</div>
						</div>
					</nav>
				</div>
				<div class="col-md-12 col-lg-9">
					<div class="breadcrumb-background"></div>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.navbar-nav {
	width: 100%;
}
</style>
