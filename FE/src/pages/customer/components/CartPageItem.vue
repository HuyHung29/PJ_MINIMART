<script setup>
import MCheckBox from "@/components/customs/MCheckBox.vue";
import { reactive } from "vue";

const props = defineProps({
	product: {
		type: Object,
		default: {},
	},
	checkList: {
		type: Array,
		default: [],
	},
});

const state = reactive({
	quantity: props.product.Quantity,
});
const emit = defineEmits(["check", "update", "delete"]);

const handleChange = (value) => {
	if (!value) {
		state.quantity = 1;
	}

	if (+value < 0) {
		state.quantity = 1;
	}

	if (+value > props.product.MainQuantity) {
		state.quantity = props.product.MainQuantity;
	}

	emit("update", {
		ProductId: props.product.ProductId,
		Quantity: state.quantity,
	});
};

const handleDelete = () => {
	emit("delete", props.product.ProductId);
};

const handleCheckBox = (data) => {
	try {
		emit("check", data.value);
	} catch (error) {
		console.log(error);
	}
};
</script>

<template>
	<div class="cart-page__item" v-if="product">
		<div class="cart-page__item__checkbox">
			<MCheckBox
				name="checkbox"
				:id="product.ProductId"
				:value="product.ProductId"
				@check="handleCheckBox"
				:checked="checkList.includes(product.ProductId)"
			/>
		</div>
		<div class="cart-page__item__product">
			<router-link :to="{ path: '/product/' + product.ProductId }">
				<img :src="product?.Pictures[0]?.Url" alt="anh" />
			</router-link>
			<router-link :to="{ path: '/product/' + product.ProductId }">
				<p class="cart-pro-title">
					{{ product.ProductName }}
					<span
						v-if="product.Quantity > product.MainQuantity"
						class="out-of-stock"
						>(Hết hàng)</span
					>
				</p>
				<p class="mt-3 cart-pro-quantity">
					Số lượng còn: {{ product.MainQuantity }}/{{
						product.UnitName
					}}
				</p>
			</router-link>
		</div>
		<div class="cart-page__item__price">
			<p>
				{{ product?.CurrentPrice?.toLocaleString() }}
				<sup>đ</sup>
			</p>
		</div>
		<div class="cart-page__item__quantity">
			<div class="cart-page__item__quantity__calculate">
				<input
					type="number"
					v-model="state.quantity"
					:debounce-events="['input', 'keyup']"
					v-debounce:500ms.lock="handleChange"
				/>
			</div>
		</div>
		<div class="cart-page__item__total">
			<p>
				{{
					(
						product?.CurrentPrice * product?.Quantity
					)?.toLocaleString()
				}}
				<sup>đ</sup>
			</p>
		</div>
		<div class="cart-page__item__action">
			<p>
				<i class="fas fa-times" @click="handleDelete"></i>
			</p>
		</div>
	</div>
</template>

<style scoped>
.cart-pro-title {
	font-weight: 500;
}

.cart-pro-quantity {
	font-size: 12px;
}

.out-of-stock {
	color: red;
}
</style>
