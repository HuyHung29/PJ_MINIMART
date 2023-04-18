<script setup>
import Col from "./bootstrap/Col.vue";
import Container from "./bootstrap/Container.vue";
import Row from "./bootstrap/Row.vue";
</script>

<template>
	<footer class="footer">
		<Container class="footer__container px-md-5">
			<Row>
				<Col md="6" lg="3" class="pb-md-4">
					<img
						src="@/assets/images/logo_footer.png"
						alt="logo"
						class="footer__logo"
					/>

					<div class="footer__info">
						<p class="footer__info__title">
							Siêu thị MiniMart cung cấp các mặt hàng siêu sạch
							đem lại sức khỏe cho bạn
						</p>
						<div class="footer__contact">
							<div class="footer__contact__item">
								<div class="footer__contact__item__img">
									<i class="fas fa-flag"></i>
								</div>
								<span class="footer__contact__item__text">
									Tầng 3, tòa nhà Techno, Duy Tân
								</span>
							</div>
							<div class="footer__contact__item">
								<div class="footer__contact__item__img">
									<i class="fas fa-phone"></i>
								</div>
								<a
									href="tel:19006750"
									class="footer__contact__item__router-link"
								>
									1900 9999
								</a>
							</div>
							<div class="footer__contact__item">
								<div class="footer__contact__item__img">
									<i class="fas fa-envelope"></i>
								</div>
								<a
									href="mailto:support@sapo.vn"
									class="footer__contact__item__router-link"
								>
									support@misa.vn
								</a>
							</div>
						</div>
					</div>
				</Col>
				<Col md="6" lg="3" class="pb-md-4">
					<ul class="footer__list">
						<h3 class="footer__list__title">Tài khoản</h3>
						<li class="footer__item">
							<router-link to="/">Trang chủ</router-link>
						</li>
						<li class="footer__item">
							<router-link to="/products">Sản phẩm</router-link>
						</li>
						<li class="footer__item">
							<router-link to="/posts">Tin tức</router-link>
						</li>
					</ul>
				</Col>
				<Col md="6" lg="3" class="pb-md-4">
					<ul class="footer__list">
						<h3 class="footer__list__title">Hỗ trợ khách hàng</h3>
						<li class="footer__item">
							<router-link to="/">Trang chủ</router-link>
						</li>
						<li class="footer__item">
							<router-link to="/products">Sản phẩm</router-link>
						</li>
						<li class="footer__item">
							<router-link to="/posts">Tin tức</router-link>
						</li>
					</ul>
				</Col>
				<Col md="6" lg="3" class="pb-md-4">
					<div class="footer__mail">
						<h3 class="footer__mail__title">Gửi mail</h3>
						<p class="footer__mail__desc">
							Gửi email nhận khuyến mãi
						</p>
						<div class="footer__mail__form">
							<div class="input-group">
								<input
									class="form-control footer__mail__input shadow-none"
									placeholder="Email của bạn"
								/>
								<button
									class="btn footer__mail__btn shadow-none"
									type="submit"
								>
									<i class="fab fa-telegram-plane"></i>
								</button>
							</div>
						</div>

						<div class="footer__social">
							<h3 class="footer__social__title">Kết nối</h3>

							<div class="footer__social__list">
								<p class="footer__social__item">
									<i class="fab fa-google-plus-g"></i>
								</p>
								<p class="footer__social__item">
									<i class="fab fa-twitter"></i>
								</p>
								<p class="footer__social__item">
									<i class="fab fa-facebook-f"></i>
								</p>
								<p class="footer__social__item">
									<i class="fa-brands fa-linkedin"></i>
								</p>
							</div>
						</div>
					</div>
				</Col>
			</Row>

			<div class="footer__copyright">
				<p>
					© Bản quyền thuộc về <span>Cafein Team</span> | Cung cấp bởi
					<span>Sapo</span>
				</p>
				<button class="btn footer__copyright__btn shadow-none">
					Lên đầu trang
					<i class="fas fa-arrow-up"></i>
				</button>
			</div>
		</Container>
	</footer>
</template>

<style scoped>
.footer {
	background-image: url(@/assets/images/bg_footer.png);
}
.footer__mail__btn {
	color: white;
}
</style>
