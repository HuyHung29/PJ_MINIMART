<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";

const props = defineProps({
	supplier: {
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
	emit("update", props.supplier);
};

const onDeleteBtn = () => {
	emit("delete", props.supplier);
};
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col text-center">
			<CheckBox
				name="c-table-checkbox"
				:id="supplier.SupplierId"
				:value="supplier.SupplierId"
				@check="handleCheckBox"
				:checked="checkList.includes(supplier.SupplierId)"
			/>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__img" ref="nameRef">
				<p class="c-table__col__text">{{ supplier.SupplierCode }}</p>
			</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">{{ supplier.SupplierName }}</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">{{ supplier.Address }}</p>
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
