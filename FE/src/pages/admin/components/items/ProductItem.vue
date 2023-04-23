<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";

const props = defineProps({
	product: {
		type: Object,
		required: true,
		default: {},
	},
	checkList: {
		type: Array,
		default: [],
	},
});

const handleCheckBox = (data) => {
	try {
		emit("check", data.value);
	} catch (error) {
		console.log(error);
	}
};

const emit = defineEmits(["check", "update", "delete"]);

const onUpdateBtnClick = () => {
	console.log("update");
	emit("update", props.product);
};

const onDeleteBtn = () => {
	emit("delete", props.product);
};
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col text-center">
			<CheckBox
				name="c-table-checkbox"
				:id="product.ProductId"
				:value="product.ProductId"
				@check="handleCheckBox"
				:checked="checkList.includes(product.ProductId)"
			/>
		</td>
		<td class="c-table__col c-w-100">
			<p class="c-table__col__img" ref="nameRef">
				{{ product.ProductCode }}
			</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__text">{{ product.ProductName }}</p>
		</td>
		<td class="c-table__col w-400">
			<p class="c-table__col__img" ref="nameRef">
				<img
					v-for="(item, index) in product.Pictures"
					:key="index"
					:src="item.Url"
					alt=""
				/>
			</p>
		</td>
		<td class="c-table__col c-w-100">
			<p class="c-table__col__text">{{ product.Price }}</p>
		</td>
		<td class="c-table__col c-w-100">
			<p class="c-table__col__img" ref="nameRef">
				{{ product.Quantity }}
			</p>
		</td>
		<td class="c-table__col c-w-100">
			<p class="c-table__col__text">{{ product.UnitName }}</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__img" ref="nameRef">
				{{ product.Weight }} {{ product.VolumeName }}
			</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__img" ref="nameRef">
				{{ product.CategoryName }}
			</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__img" ref="nameRef">
				{{ product.SupplierName }}
			</p>
		</td>

		<td class="c-table__col text-center">
			<div class="c-table__action">
				<p class="c-table__action__update" @click="onUpdateBtnClick">
					Sửa
				</p>
				<p
					class="c-table__action__update btn-delete"
					@click="onDeleteBtn"
				>
					Xóa
				</p>
			</div>
		</td>
	</tr>
</template>

<style scoped>
.c-table__action__update.btn-delete {
	color: red !important;
	margin-left: 15px;
}
</style>
