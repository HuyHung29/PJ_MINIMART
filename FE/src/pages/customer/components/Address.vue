<script setup>
import AddressAddEdit from "./AddressAddEdit.vue";
</script>

<template>
	<div class="profile">
		<div class="profile__header profile__header--flex">
			<h2 class="profile__heading">Địa chỉ của tôi</h2>
			<button class="btn btn-normal">
				<i class="fas fa-plus"></i> Thêm địa chỉ mới
			</button>
		</div>
		<ul class="profile__address__list">
			<li class="profile__address__item" key="{index}">
				<div class="profile__address__item__info">
					<div class="profile__user__field">
						<p class="profile__user__label">Họ và tên</p>
						<p class="profile__user__data">{item.name}</p>
					</div>
					<div class="profile__user__field">
						<p class="profile__user__label">Số điện thoại</p>
						<p class="profile__user__data">{item.phone}</p>
					</div>

					<div class="profile__user__field">
						<p class="profile__user__label">Địa chỉ</p>
						<p class="profile__user__data"></p>
					</div>
				</div>
				<div class="profile__address__item__action">
					<p>Sửa</p>
					<p>Xóa</p>
				</div>
			</li>
		</ul>
		<AddressAddEdit />
	</div>
</template>

<style scoped>
.profile__address__list {
	max-height: 300px;
	overflow-y: auto;
}
</style>
