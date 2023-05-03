<script setup>
import moment from "moment";

const props = defineProps({
	user: {
		type: Object,
		required: true,
		default: {},
	},
	checkList: {
		type: Array,
		default: [],
	},
});
</script>

<template>
	<tr class="c-table__row">
		<td class="c-table__col w-200">
			<router-link
				:to="{
					path: '/admin/order',
					query: { AccountId: user.AccountId },
				}"
				class="c-table__col__text"
				>{{ user.UserName }}</router-link
			>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">{{ user.FullName }}</p>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">{{ user.Email }}</p>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">{{ user.PhoneNumber }}</p>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">{{ user.GenderName }}</p>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">
				{{ moment(user.DOB).format("DD/MM/YYYY") }}
			</p>
		</td>
		<td class="c-table__col w-250">
			<p class="c-table__col__text">{{ user.Address }}</p>
		</td>
	</tr>
</template>

<style scoped>
.c-table__action__update.btn-delete {
	color: red !important;
	margin-left: 15px;
}
</style>
