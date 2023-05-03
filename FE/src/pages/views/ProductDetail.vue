<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Container from "@/components/bootstrap/Container.vue";
import Row from "@/components/bootstrap/Row.vue";
import { useRoute, useRouter } from "vue-router";
import { onBeforeMount, reactive, computed } from "vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import MButton from "@/components/MButton.vue";
import ProductSlider from "@/components/ProductSlider.vue";

const store = useStore();

const route = useRoute();
const router = useRouter();

const { useState, useActions } = createNamespacedHelpers(store, "product");

const { currentItem, product } = useState(["currentItem", "product"]);

const { getProduct, fetchProduct } = useActions(["getProduct", "fetchProduct"]);

const cartStore = createNamespacedHelpers(store, "cart");

const { cart } = cartStore.useState(["cart"]);

const { insert } = cartStore.useActions(["insert"]);

const initData = async () => {
	await getProduct(route.params.id);
	await fetchProduct({ CategoryId: currentItem.value.CategoryId });
};

onBeforeMount(() => {
	initData();
});

const state = reactive({
	img: currentItem?.value?.OldPictures[0]?.Url || "",
	value: 1,
	isInCart: false,
});

const setImg = (src) => {
	state.img = src;
};

const plus = () => {
	if (state.value < currentItem.value.Quantity) {
		state.value += 1;
	} else {
		state.value = currentItem.value.Quantity;
	}
};

const minus = () => {
	if (state.value > 1) {
		state.value -= 1;
	}
};

const handleChange = (e) => {
	const value = e.target.value;
	if (+value < 1) {
		state.value = 1;
	}

	if (+value > currentItem.value.Quantity) {
		state.value = currentItem.value.Quantity;
	}
};

const isInCart = computed(() => {
	if (cart.value && currentItem.value) {
		return (
			cart.value.findIndex(
				(item) => item.ProductId === currentItem.value.ProductId
			) >= 0
		);
	}
});

const handleAddToCart = async () => {
	if (store.state.user.isLogin) {
		const newPro = {
			...currentItem.value,
			MainQuantity: currentItem.value.Quantity,
			Quantity: +state.value,
			Pictures: currentItem.value.OldPictures,
		};
		await insert(newPro);
	} else {
		router.push("/login");
	}
};
</script>

<template>
	<Container class="product-detail px-md-5" v-if="currentItem">
		<Row>
			<Col md="12">
				<section class="bg-white product-detail__page">
					<Row>
						<Col md="4">
							<div
								class="image-slider"
								v-if="currentItem.OldPictures"
							>
								<img
									:src="
										state.img ||
										currentItem.OldPictures[0].Url
									"
								/>
								<div class="sub-img">
									<img
										v-for="(
											item, index
										) in currentItem.OldPictures"
										:key="index"
										:src="item.Url"
										@click="() => setImg(item.Url)"
										:class="{
											active: state.img === item.Url,
										}"
									/>
								</div>
							</div>
						</Col>

						<Col md="8">
							<div class="product-detail__page__info">
								<h2>
									{{ currentItem.ProductName }}
									<span
										v-if="currentItem.Quantity == 0"
										class="out-of-stock"
										>(Hết hàng)</span
									>
								</h2>

								<div
									class="product-detail__page__info__evaluate"
								>
									<div
										class="product-detail__page__info__evaluate__item"
									>
										<p>
											5
											<i class="fas fa-star"></i>
											<i class="fas fa-star"></i>
											<i class="fas fa-star"></i>
											<i class="fas fa-star"></i>
											<i class="fas fa-star"></i>
										</p>
									</div>
									<div
										class="product-detail__page__info__evaluate__item"
									>
										<p>0 <span>đánh giá</span></p>
									</div>
									<div
										class="product-detail__page__info__evaluate__item"
									>
										<p>
											{{ currentItem.Quantity }}
											<span>{{
												currentItem.UnitName
											}}</span>
										</p>
									</div>
								</div>

								<div class="product-detail__page__info__bg">
									<p
										class="product__price--old product__price--detail--old"
										v-if="currentItem.Discount != 0"
									>
										<sup>đ</sup>
										{{ currentItem.Price }}
									</p>
									<p
										class="product__price product__price--detail"
										v-if="currentItem.Discount != 0"
									>
										<sup>đ</sup>

										{{
											currentItem.CurrentPrice.toLocaleString()
										}}
									</p>
									<span
										class="product__price__discount"
										v-if="currentItem.Discount != 0"
									>
										{{
											currentItem.Discount.toLocaleString()
										}}% GIẢM
									</span>

									<p
										class="product__price product__price--detail"
										v-if="currentItem.Discount === 0"
									>
										<sup>đ</sup>
										{{
											currentItem.CurrentPrice.toLocaleString()
										}}
									</p>
								</div>

								<div class="d-flex align-items-center mt-4">
									<p class="product-detail__page__info__text">
										Phân loại
									</p>
									<p
										class="product-detail__page__info__text--dark"
									>
										{{ currentItem.CategoryName }}
									</p>
								</div>

								<div class="d-flex align-items-center mt-4">
									<p class="product-detail__page__info__text">
										Xuất sứ
									</p>
									<p
										class="product-detail__page__info__text--dark"
									>
										{{ currentItem.SupplierName }}
									</p>
								</div>

								<div class="d-flex align-items-center mt-4">
									<p class="product-detail__page__info__text">
										Khối lượng
									</p>
									<p
										class="product-detail__page__info__text--dark"
									>
										{{ currentItem.Weight }}
										{{ currentItem.VolumeName }} /
										{{ currentItem.UnitName }}
									</p>
								</div>

								<div
									class="product-detail__page__info__quantity"
								>
									<p class="product-detail__page__info__text">
										Số lượng
									</p>
									<div class="product__quantity">
										<p
											class="product__quantity__btn"
											@click="minus"
										>
											<i class="fas fa-minus"></i>
										</p>
										<input
											type="text"
											v-model="state.value"
											name="value"
											class="product__quantity__value"
											@blur="handleChange"
										/>
										<p
											class="product__quantity__btn"
											@click="plus"
										>
											<i class="fas fa-plus"></i>
										</p>
									</div>
								</div>

								<div class="detail-footer">
									<MButton
										content="Đã có trong giỏ hàng"
										v-if="isInCart"
										:disabled="true"
									/>
									<MButton
										content="Thêm vào giỏ hàng"
										v-if="
											!isInCart &&
											currentItem.Quantity > 0
										"
										@click="handleAddToCart"
									/>

									<MButton
										content="Hết hàng"
										v-if="currentItem.Quantity === 0"
									/>

									<div
										class="product-detail__page__info__share"
									>
										<h3
											class="product-detail__page__info__share__title"
										>
											Chia sẻ
										</h3>

										<div
											class="product-detail__page__info__share__list"
										>
											<p
												class="product-detail__page__info__share__item"
											>
												<i
													class="fab fa-facebook-messenger"
												></i>
											</p>
											<p
												class="product-detail__page__info__share__item"
											>
												<i class="fab fa-twitter"></i>
											</p>
											<p
												class="product-detail__page__info__share__item"
											>
												<i
													class="fab fa-facebook-f"
												></i>
											</p>
											<p
												class="product-detail__page__info__share__item"
											>
												<i class="fab fa-pinterest"></i>
											</p>
										</div>
									</div>
								</div>
							</div>
						</Col>
					</Row>
				</section>
			</Col>

			<Col md="12">
				<section class="bg-white product-detail__page">
					<div class="product-detail__page__info__bg">
						<h2>Mô tả sản phẩm</h2>
					</div>
					<div
						class="description"
						v-html="currentItem.Description"
					></div>
				</section>
			</Col>

			<Col md="12">
				<h2 class="slider__title">Sản phẩm cùng loại</h2>
				<section class="bg-white product-detail__page">
					<div class="">
						<ProductSlider :product="product" />
					</div>

					<p v-if="product.length == 0">
						Không có sản phẩm nào cùng loại
					</p>
				</section>
			</Col>
		</Row>
	</Container>
</template>

<style scoped>
.product-detail__page__info h2 {
	font-size: 30px;
	font-weight: bold;
}

.detail-footer {
	margin-top: 20px;
	display: flex;
	align-items: center;
	justify-content: space-between;
}

.product-detail__page__info__share {
	margin-top: 0;
}

.thumbnails {
	margin: auto;
	max-width: 300px;
}

.thumbnails .vueperslide {
	box-sizing: border-box;
	border: 1px solid #fff;
	transition: 0.3s ease-in-out;
	opacity: 0.7;
	cursor: pointer;
}

.thumbnails .vueperslide--active {
	box-shadow: 0 0 6px rgba(0, 0, 0, 0.3);
	opacity: 1;
	border-color: #000;
}

.sub-img {
	display: flex;
	align-items: center;
	justify-content: center;
	max-width: 100%;
	overflow-y: auto;
	padding: 10px;

	column-gap: 10px;
}

.sub-img img {
	width: 80px;
	object-fit: contain;
	border: 1px solid #cccccc;
}

.sub-img img.active {
	border-color: red;
}

.product-detail__page__info__bg h2 {
	font-size: 20px;
	font-weight: bold;
}

.out-of-stock {
	color: red;
}
</style>
