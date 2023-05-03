<script setup>
import RESOURCES from "../constants/resource";
import { computed, inject, reactive, watch } from "@vue/runtime-core";
import MSelect from "@/components/customs/MSelect.vue";
import { useRouter, useRoute } from "vue-router";
import { ref } from "vue";
import Paginate from "vuejs-paginate-next";

const props = defineProps({
	pagination: {
		type: Object,
		default: {},
	},
});

const route = useRoute();
const router = useRouter();

const page = ref(props.pagination.PageNumber);

const handleChange = (page) => {
	console.log(page);
};

const handleChangePageSize = (value) => {
	router.replace({
		path: route.path,
		query: {
			...route.query,
			PageSize: value,
			PageNumber: page.value,
		},
	});
};
</script>

<template>
	<div class="data-table__footer">
		<p class="data-table__quantity">
			Tổng số: <span>{{ pagination.TotalRecord }}</span> bản ghi
		</p>

		<div class="data-table__action">
			<MSelect
				:options="RESOURCES.PAGINATION"
				:style="{ top: 'unset', bottom: '100%' }"
				:readonly="true"
				:defaultValue="
					RESOURCES.PAGINATION.find(
						(pageSize) => pageSize.value == pagination.PageSize
					).title
				"
				@select="handleChangePageSize"
			/>
			<!-- Pagination -->
			<div class="data-table__pagination">
				<paginate
					v-model="page"
					:page-count="pagination.TotalPage"
					:page-range="3"
					:margin-pages="1"
					:prev-text="'Trước'"
					:next-text="'Sau'"
					:container-class="'pagination'"
					:page-class="'page-item'"
					:prev-class="'prev-btn'"
					:next-class="'next-btn'"
					:click-handler="handleChange"
				>
				</paginate>
			</div>
		</div>
	</div>
</template>

<style scoped>
.data-table__quantity {
	font-size: 14px;
}
</style>
