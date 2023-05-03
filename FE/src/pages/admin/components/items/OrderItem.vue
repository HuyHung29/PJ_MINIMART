<script setup>
import moment from "moment";
import RESOURCE from "@/constants/resource";
import MButton from "@/components/MButton.vue";

const props = defineProps({
	order: {
		type: Object,
		required: true,
		default: {},
	},
});

const emit = defineEmits(["update"]);

const handleUpdate = (value) => {
	emit("update", {
		OrderId: props.order.OrderId,
		AccountId: props.order.AccountId,
		Status: value,
	});
};
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col w-250">
			<p class="c-table__col__text">Người nhận: {{ order.Receiver }}</p>
			<p class="c-table__col__text">
				Số điện thoại: {{ order.PhoneNumber }}
			</p>
		</td>
		<td class="c-table__col w-400">
			<div class="order_product-wrap">
				<div
					class="order_product"
					v-for="(item, index) in order.Products"
					:key="index"
				>
					<img class="order_product__img" :src="item.Url" alt="" />
					<p>
						{{ item.ProductName }}
						({{ item.Quantity }} {{ item.UnitName }})
					</p>
				</div>
			</div>
		</td>
		<td class="c-table__col text-center w-150">
			<p class="c-table__col__text">
				{{ moment(order.CreatedDate).format("DD/MM/YYYY") }}
			</p>
		</td>
		<td class="c-table__col text-center w-150">
			<p class="c-table__col__text">
				{{ order.Total.toLocaleString() }} VNĐ
			</p>
		</td>
		<td class="c-table__col w-400">
			<p class="c-table__col__text">
				{{ order.DeliveryPlace }}
			</p>
		</td>
		<td class="c-table__col text-center w-150">
			<p class="c-table__col__text">
				{{ order.Note || "Không có ghi chú" }}
			</p>
		</td>
		<td class="c-table__col text-center w-150">
			<p
				class="c-table__col__text"
				:class="{
					_0: order.Status == RESOURCE.ORDER_STATUS.Waiting,
					_1: order.Status == RESOURCE.ORDER_STATUS.Delivered,
					_2: order.Status == RESOURCE.ORDER_STATUS.Done,
					_3: order.Status == RESOURCE.ORDER_STATUS.Cancled,
				}"
			>
				{{ order.StatusName }}
				<MButton
					class="order_status"
					v-if="order.Status == RESOURCE.ORDER_STATUS.Waiting"
					content="Đang vận chuyển"
					@click="() => handleUpdate(RESOURCE.ORDER_STATUS.Delivered)"
				/>
				<MButton
					class="order_status"
					v-if="order.Status == RESOURCE.ORDER_STATUS.Delivered"
					content="Đã giao"
					@click="() => handleUpdate(RESOURCE.ORDER_STATUS.Done)"
				/>
			</p>
		</td>
	</tr>
</template>

<style scoped>
.c-table__action__update.btn-delete {
	color: red !important;
	margin-left: 15px;
}

.order_product-wrap {
	max-height: 150px;
	overflow-y: auto;
}

.order_product {
	display: flex;
	align-items: center;
	margin-bottom: 10px;
}

.order_product__img {
	width: 50px;
	border: 1px solid #ccc;
	margin-right: 10px;
}
.order_status {
	display: none;
	position: absolute;
	top: calc(100% + 10px);
	left: 0;
	right: 0;
}

.c-table__col__text {
	position: relative;

	cursor: pointer;
}

.c-table__col__text::before {
	content: "";
	position: absolute;
	display: block;
	top: 100%;
	left: 0;
	right: 0;
	height: 10px;
	background-color: transparent;
}

.c-table__col__text:hover .order_status {
	display: block;
}

.c-table__col__text {
	padding: 4px 0;

	border-radius: 5px;
}

.c-table__col__text._0 {
	color: white;
	background-color: orange;
}

.c-table__col__text._1 {
	color: white;
	background-color: blue;
}

.c-table__col__text._2 {
	color: white;
	background-color: green;
}

.c-table__col__text._3 {
	color: white;
	background-color: red;
}
</style>
