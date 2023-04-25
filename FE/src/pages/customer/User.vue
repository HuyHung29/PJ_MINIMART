<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import { RouterView, useRoute } from "vue-router";
import { ref } from "vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const open = ref(false);

const route = useRoute();

const store = useStore();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"user"
);

const { user } = useState(["user"]);
</script>

<template>
	<Container class="user px-md-5">
		<Row>
			<Col class="d-none d-lg-block" lg="2">
				<div class="user__nav">
					<div class="user__nav__header">
						<div class="user__nav__img">
							<i class="fas fa-user"></i>
						</div>
						<div class="user__nav__basic-info">
							<p class="user__nav__text">{{ user.FullName }}</p>
							<router-link
								to="/user/profile"
								class="user__nav__sub-text"
							>
								<i class="fas fa-pen"></i> Sửa hồ sơ
							</router-link>
						</div>
					</div>

					<ul class="user__nav__list">
						<li class="user__nav__item">
							<i class="fas fa-user"></i>
							<router-link
								to="/user/profile"
								class="user__nav__link"
								@click="open = !open"
							>
								Tài khoản của tôi
							</router-link>
							<ul
								class="user__nav__sub-nav"
								:class="{
									open: open,
								}"
							>
								<li class="user__nav__item sub-item">
									<router-link
										to="/user/profile"
										class="user__nav__link"
										:class="{
											active: route.path.includes(
												'profile'
											),
										}"
									>
										Hồ sơ
									</router-link>
								</li>
								<li class="user__nav__item sub-item">
									<router-link
										to="/user/address"
										class="user__nav__link"
										:class="{
											active: route.path.includes(
												'address'
											),
										}"
									>
										Địa chỉ
									</router-link>
								</li>
								<li class="user__nav__item sub-item">
									<router-link
										to="/user/password"
										class="user__nav__link"
										:class="{
											active: route.path.includes(
												'password'
											),
										}"
									>
										Đổi mật khẩu
									</router-link>
								</li>
							</ul>
						</li>

						<li class="user__nav__item">
							<i class="fas fa-clipboard"></i>
							<router-link
								exact
								to="/user/order"
								class="user__nav__link"
							>
								Đơn hàng
							</router-link>
						</li>
					</ul>
				</div>
			</Col>
			<Col>
				<div class="user__content">
					<RouterView></RouterView>
				</div>
			</Col>
		</Row>

		<div class="user__setting__btn d-block d-lg-none">
			<i class="fa-solid fa-gear"></i>
			<div class="user__nav shadow">
				<ul class="user__nav__list">
					<li class="user__nav__item">
						<i class="fas fa-user"></i>
						<router-link to="/user/profile" class="user__nav__link">
							Tài khoản của tôi
						</router-link>
						<ul class="user__nav__sub-nav open">
							<li class="user__nav__item sub-item">
								<router-link
									to="/user/profile"
									class="user__nav__link"
									activeclass="active"
								>
									Hồ sơ
								</router-link>
							</li>
							<li class="user__nav__item sub-item">
								<router-link
									to="/user/address"
									class="user__nav__link"
									activeclass="active"
								>
									Địa chỉ
								</router-link>
							</li>
							<li class="user__nav__item sub-item">
								<router-link
									to="/user/password"
									class="user__nav__link"
									activeclass="active"
								>
									Đổi mật khẩu
								</router-link>
							</li>
						</ul>
					</li>

					<li class="user__nav__item">
						<i class="fas fa-clipboard"></i>
						<router-link
							exact
							to="/user/orders"
							class="user__nav__link"
						>
							Đơn hàng
						</router-link>
					</li>
				</ul>
			</div>
		</div>
	</Container>
</template>

<style scoped>
.user__nav__sub-text {
	display: block;
	margin-top: 10px;
}
</style>
