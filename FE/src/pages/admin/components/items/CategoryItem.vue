<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";

import { useRouter, useRoute } from "vue-router";

const props = defineProps({
	category: {
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
	emit("update", props.category);
};

const onDeleteBtn = () => {
	emit("delete", props.category);
};
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col text-center">
			<CheckBox
				name="c-table-checkbox"
				:id="category.CategoryId"
				:value="category.CategoryId"
				@check="handleCheckBox"
				:checked="checkList.includes(category.CategoryId)"
			/>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__img" ref="nameRef">
				<img :src="category.Thumbnail" alt="" />
			</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">{{ category.CategoryName }}</p>
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
