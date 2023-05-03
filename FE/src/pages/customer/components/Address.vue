<script setup>
import { useStore } from "vuex";
import AddressAddEdit from "./AddressAddEdit.vue";
import { reactive, onBeforeMount } from "vue";
import { createNamespacedHelpers } from "vuex-composition-helpers";

const store = useStore();

const { useState, useActions } = createNamespacedHelpers(store, "address");

const { address } = useState(["address"]);

const { fetchAddress, remove } = useActions(["fetchAddress", "remove"]);

const initData = async () => {
	await fetchAddress();
};

const state = reactive({
	isShowForm: false,
	address: null,
});

const handleAddAddress = () => {
	state.isShowForm = true;
};
const handleClose = () => {
	state.isShowForm = false;
	state.address = null;
};

onBeforeMount(() => {
	initData();
});

const handleDelete = async (item) => {
	await remove([item.AddressId]);
};
</script>

<template>
	<div class="profile">
		<div class="profile__header profile__header--flex">
			<h2 class="profile__heading">Địa chỉ của tôi</h2>
			<button class="btn btn-normal" @click="handleAddAddress">
				<i class="fas fa-plus"></i> Thêm địa chỉ mới
			</button>
		</div>
		<ul class="profile__address__list" v-if="address.length > 0">
			<li
				class="profile__address__item"
				v-for="(item, index) in address"
				:key="index"
			>
				<div class="profile__address__item__info">
					<div class="profile__user__field">
						<p class="profile__user__label">Họ và tên</p>
						<p class="profile__user__data">{{ item.Receiver }}</p>
					</div>
					<div class="profile__user__field">
						<p class="profile__user__label">Số điện thoại</p>
						<p class="profile__user__data">
							{{ item.PhoneNumber }}
						</p>
					</div>

					<div class="profile__user__field">
						<p class="profile__user__label">Địa chỉ</p>
						<p class="profile__user__data">
							{{
								item.Detail +
								", " +
								item.Ward +
								", " +
								item.District +
								", " +
								item.City
							}}
						</p>
					</div>
				</div>
				<div class="profile__address__item__action">
					<p @click="() => handleDelete(item)">Xóa</p>
				</div>
			</li>
		</ul>
		<AddressAddEdit v-if="state.isShowForm" @close="handleClose" />
	</div>
</template>

<style scoped>
.profile__address__list {
	max-height: 300px;
	overflow-y: auto;
}
</style>
