<script setup>
import Container from "@/components/bootstrap/Container.vue";
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
import { useStore } from "vuex";
import { createNamespacedHelpers } from "vuex-composition-helpers";
import { onBeforeMount, computed, reactive } from "vue";
import { useRouter } from "vue-router";
import MSelect from "@/components/customs/MSelect.vue";
import { useForm } from "vee-validate";
import RESOURCES from "@/constants/resource";

const router = useRouter();

const store = useStore();

const addressStore = createNamespacedHelpers(store, "address");

const { address } = addressStore.useState(["address"]);
const { fetchAddress } = addressStore.useActions(["fetchAddress"]);

const cartStore = createNamespacedHelpers(store, "cart");

const { cart } = cartStore.useState(["cart"]);

const { fetchCart } = cartStore.useActions(["fetchCart"]);

const userStore = createNamespacedHelpers(store, "user");

const { user, isLogin } = userStore.useState(["user", "isLogin"]);

const uiStore = createNamespacedHelpers(store, "ui");

const { handleOpenModal } = uiStore.useMutations(["handleOpenModal"]);

const orderStore = createNamespacedHelpers(store, "order");

const { insert } = orderStore.useActions(["insert"]);

const initData = async () => {
	await fetchAddress();
	await fetchCart();
};

onBeforeMount(() => {
	if (!isLogin.value) {
		router.replace("/login");
	} else {
		initData();
	}
});

const total = computed(() => {
	return cart.value
		? cart.value?.reduce((prev, curr) => {
				return prev + curr.CurrentPrice * curr.Quantity;
		  }, 0)
		: 0;
});

const options = computed(() => {
	return address?.value?.map((add) => {
		const rawAdd = `${add.Detail}, ${add.Ward}, ${add.District}, ${add.City}`;

		return {
			value: add.AddressId,
			title: rawAdd,
		};
	});
});

const handleSelect = (value) => {
	const add = address.value.find((item) => item.AddressId === value);

	state.DeliveryPlace = options.value.find(
		(item) => item.value === value
	).title;

	state.Receiver = add.Receiver;

	state.PhoneNumber = add.PhoneNumber;

	state.Error = null;
};

const state = reactive({
	Receiver: "",
	DeliveryPlace: "",
	PhoneNumber: "",
	Note: "",
	Error: null,
});

const routerToCart = () => {
	router.replace("/cart");
};

const handleSubmit = async () => {
	const isOutOfStock = cart.value.find(
		(item) => item.Quantity > item.MainQuantity
	);

	if (!state.DeliveryPlace) {
		state.Error = "Chọn địa chỉ nhận hàng";
		return;
	}

	if (isOutOfStock) {
		handleOpenModal({
			type: RESOURCES.MODAL_TYPE.ERROR,
			content:
				"Sản phẩm bạn chọn đã hết hàng vùi lòng xóa khỏi giỏ hàng trước khi đặt hàng",
			title: "Đặt hàng thất bại",
			callback: routerToCart,
		});
		return;
	} else {
		const data = {
			...state,
			Status: RESOURCES.ORDER_STATUS.Waiting,
			Products: [...cart.value],
		};

		const res = await insert(data);

		if (res) {
			router.replace("/user/order");
		}
	}
};
</script>

<template>
	<div class="layout">
		<div class="authen__header">
			<Container>
				<div
					class="d-flex align-items-center justify-content-between authen__header--wrap"
				>
					<div class="d-flex align-items-center">
						<router-link to="/">
							<img src="@/assets/images/logo.png" alt="logo" />
						</router-link>
						<h1 class="authen__header__title">Đặt hàng</h1>
					</div>
					<p class="authen__header__text">
						Thông tin mua hàng của bạn!
					</p>
				</div>
			</Container>
		</div>
		<div class="layout__content__background">
			<Container>
				<Row>
					<Col md="12" class="bg-white shadow-sm p-5 my-5">
						<div class="add-edit__header">
							<h1 class="add-edit__heading">Đặt hàng</h1>
							<p class="add-edit__sub-heading">
								Vui lòng điền thông tin người nhận
							</p>
						</div>
						<Row class="user__purchase--wrap">
							<Col md="6">
								<h2 class="purchase-header">
									Thông tin người nhận
								</h2>
								<div class="purchase-form">
									<Row class="purchase-row">
										<Col lg="12">
											<MSelect
												placeholder="Chọn địa chỉ nhận hàng"
												:options="options"
												@select="handleSelect"
												:error="state.Error"
											/>
										</Col>
										<Col lg="6">
											<MInput
												:placeholder="
													state.Receiver ||
													'Người nhận'
												"
												name="Receiver"
												readonly
											/>
										</Col>
										<Col lg="6">
											<MInput
												:placeholder="
													state.PhoneNumber ||
													'Số điện thoại'
												"
												name="PhoneNumber"
												readonly
											/>
										</Col>

										<Col lg="12">
											<div class="mb-3 shadow-none">
												<textarea
													class="form-control textarea"
													id="exampleFormControlTextarea1"
													rows="6"
													placeholder="Ghi chú"
													v-model="state.Note"
												></textarea>
											</div>
										</Col>
										<Col lg="12" class="purchase-btn">
											<button
												type="submit"
												class="buy-btn"
												@click="handleSubmit"
											>
												Đặt hàng
											</button>
										</Col>
									</Row>
								</div>
							</Col>
							<Col md="6">
								<div class="product-cart__list">
									<h2 class="product-cart__heading">
										Đơn hàng ({{ cart.length }} sản phẩm)
									</h2>
									<div class="product-cart__item--wrap">
										<div
											class="product-cart__item"
											v-for="(item, index) in cart"
											:key="index"
										>
											<div
												class="product-cart__item__img"
											>
												<img
													:src="item.Pictures[0].Url"
													alt="anh"
												/>
												<span>{{ item.Quantity }}</span>
											</div>
											<p
												class="product-cart__item__title"
											>
												{{ item.ProductName }}
												<span
													v-if="
														item.Quantity >
														item.MainQuantity
													"
													class="out-of-stock"
													>(Hết hàng)</span
												>
											</p>
											<p
												class="product-cart__item__total"
											>
												{{
													(
														item.Quantity *
														item.CurrentPrice
													).toLocaleString()
												}}
												<sup>đ</sup>
											</p>
										</div>
									</div>
									<div class="product-cart__sum">
										<p>Tổng cộng</p>
										<p class="product-cart__sum__price">
											{{ total.toLocaleString() }}
											<sup>đ</sup>
										</p>
									</div>
									<!-- <p class="product-cart__error" ></p> -->
									<input
										type="hidden"
										class="d-none"
										name="total_money"
										value="<?=$sum?>"
									/>
									<div class="product-cart__action">
										<router-link to="/cart">
											<i
												class="fa-solid fa-chevron-left"
											></i>
											Quay về giỏ hàng
										</router-link>
									</div>
								</div>
							</Col>
						</Row>
					</Col>
				</Row>
			</Container>
		</div>
	</div>
</template>

<style scoped>
.layout__content__background {
	min-height: calc(100vh - 107px);
}

.purchase-header {
	font-size: 24px;
	margin-bottom: 10px;
}

.add-edit__heading {
	font-size: 26px;
	margin-bottom: 10px;
}

.purchase-row {
	row-gap: 20px;
	height: 100%;
}

.textarea {
	font-size: 14px;
}

.textarea::placeholder {
	font-style: italic;
}

.out-of-stock {
	color: red;
}
</style>
