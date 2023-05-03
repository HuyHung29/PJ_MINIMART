<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import moment from "moment";

const props = defineProps({
	news: {
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
	emit("update", props.news);
};

const onDeleteBtn = () => {
	emit("delete", props.news);
};
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col text-center">
			<CheckBox
				name="c-table-checkbox"
				:id="news.NewsId"
				:value="news.NewsId"
				@check="handleCheckBox"
				:checked="checkList.includes(news.NewsId)"
			/>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__img" ref="nameRef">
				<img :src="news.Thumbnail" alt="" />
			</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">{{ news.Title }}</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">
				{{ moment(news.CreatedDate).format("DD/MM/YYYY") }}
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
