<script setup>
import ProductSlider from "@/components/ProductSlider.vue";
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
</script>

<template>
	<div class="top-slider">
		<img src="@/assets/images/slider_1.jpeg" alt="slider" />
	</div>
	<Container class="home px-md-5">
		<Row class="home__banner">
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_1.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_2.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col xs="12" sm="12" md="4" class="mb-3" key="{index}">
				<div class="home__banner__item">
					<img
						src="@/assets/banner/banner_3.jpg"
						alt="banner"
						class="home__banner__img"
					/>
				</div>
			</Col>
			<Col md="12" class="home__slider">
				<div class="home__link--wrap">
					<router-link to="" class="home__link">
						Hoa quar
					</router-link>
				</div>
				<!-- <div
					class="d-none d-sm-none d-md-block d-lg-none overflow-hidden"
				>
					<ProductSlider />
				</div> -->
				<!-- <div class="d-sm-block d-md-none d-lg-none overflow-hidden">
					<ProductSlider />
				</div> -->
				<div
					class="d-none d-sm-none d-md-none d-lg-block overflow-hidden"
				>
					<ProductSlider />
				</div>
			</Col>
			<Col md="12" class="home__post">
				<div class="home__link--wrap">
					<Link to="/posts" class="home__link">
						Mẹo hay sử dụng
					</Link>
				</div>
				<div class="home__post__list">
					<Row>
						<Col md="12" lg="4">
							<div class="home__post__item">
								<router-link
									class="home__post__item__img"
									to=""
								>
									<img
										src="https://nhandaovadoisong.com.vn/wp-content/uploads/2019/07/nhung-cau-noi-hay-ve-su-co-gang-6.jpg"
										alt="anh"
										title="{post.title}"
									/>
								</router-link>
								<div class="home__post__item__content">
									<router-link
										class="home__post__item__link"
										to=""
										title="{post.title}"
									>
										Tên bài viết
									</router-link>
									<div
										class="product-card__name read-more"
										:style="{
											WebkitLineClamp: 4,
											display: '-webkit-box',
										}"
									>
										Lorem ipsum dolor sit amet, consectetur
										adipisicing elit. Adipisci in iste quod
										reiciendis quam cumque! Necessitatibus
										id dolorem quos suscipit, minima vero
										perspiciatis, incidunt quis quisquam
										eligendi earum, ullam pariatur.
									</div>
								</div>
							</div>
						</Col>
					</Row>
				</div>
			</Col>
		</Row>
	</Container>
</template>

<style></style>
