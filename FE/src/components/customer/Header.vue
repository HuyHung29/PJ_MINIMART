<script setup>
import { createNamespacedHelpers } from "vuex-composition-helpers";
import Col from "../bootstrap/Col.vue";
import Container from "../bootstrap/Container.vue";
import Row from "../bootstrap/Row.vue";
import { useStore } from "vuex";
import { useToast } from "vue-toastification";
import { useRouter } from "vue-router";
import Cart from "../Cart.vue";

const store = useStore();
const router = useRouter();

const toast = useToast();

const { useState, useActions, useMutations } = createNamespacedHelpers(
	store,
	"user"
);

const { user, isLogin } = useState(["user", "isLogin"]);

const { logout } = useMutations(["logout"]);
</script>

<template>
	<header class="header">
		<Container>
			<div class="header__top d-none d-lg-flex">
				<div class="header__top__side">
					<p class="header__top__item header__top__item--social">
						<span>Kết nối</span>
						<a
							href="https://www.facebook.com/profile.php?id=100011702486663"
							target="_blank"
							rel="noopener noreferrer"
						>
							<i class="header__top__icon fab fa-facebook"></i>
						</a>
						<a
							href="https://www.facebook.com/profile.php?id=100011702486663"
							target="_blank"
							rel="noopener noreferrer"
						>
							<i class="header__top__icon fab fa-instagram"></i>
						</a>
					</p>
				</div>
				<div class="header__top__side">
					<a
						href="https://www.facebook.com/profile.php?id=100011702486663"
						target="_blank"
						rel="noopener noreferrer"
						class="header__top__item header__top__item--none"
					>
						<i class="header__top__icon far fa-question-circle"></i>
						Hỗ trợ
					</a>

					<div class="header__top__item" v-if="isLogin">
						<i class="header__top__icon fas fa-user-circle"></i>
						<p>{{ user.UserName || "Huy Huwng" }}</p>

						<ul class="header__top__item--sub">
							<li class="header__top__item--sub__item">
								<router-link to="/user">
									Tài khoản của tôi
								</router-link>
							</li>
							<li class="header__top__item--sub__item">
								<router-link to="/user/orders"
									>Đơn mua</router-link
								>
							</li>
							<li
								class="header__top__item--sub__item"
								@click="logout"
							>
								Đăng xuất
							</li>
						</ul>
					</div>

					<div
						class="d-flex justify-content-center align-items-center"
						v-else
					>
						<router-link to="/register" class="header__top__item">
							Đăng ký
						</router-link>
						<router-link to="/login" class="header__top__item">
							Đăng nhập
						</router-link>
					</div>
				</div>
			</div>
			<div class="header__middle">
				<Row class="align-content-center">
					<Col xs="12" sm="12" lg="3" class="header__middle__logo">
						<router-link to="/">
							<img src="@/assets/images/logo.png" alt="logo" />
						</router-link>
					</Col>
					<Col
						xs="12"
						sm="12"
						md="9"
						lg="7"
						class="header__middle__search"
					>
						<div class="form-wrap">
							<Form class="header__middle__form">
								<div class="input-group">
									<input
										name="search"
										class="form-control header__middle__form__input"
										placeholder="Tìm kiếm sản phẩm"
									/>
									<button
										class="header__middle__form__button"
									>
										<i class="fas fa-search"></i>
									</button>
								</div>
							</Form>
						</div>
					</Col>
					<Col xs="12" sm="12" md="3" lg="2">
						<Cart />
					</Col>
				</Row>
			</div>
		</Container>
	</header>
</template>

<style></style>
