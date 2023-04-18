<script setup>
import MInput from "@/components/customs/MInput.vue";
</script>

<template>
	<div class="profile">
		<div class="profile__header">
			<h2 class="profile__heading">Đổi mật khẩu</h2>
			<p class="profile__sub-heading">
				Để bảo mật tài khoản, vui lòng không chia sẻ mật khẩu cho người
				khác
			</p>
		</div>
		<form
			class="profile__user d-flex"
			id="profile-form"
			onSubmit="{handleSubmit(onUserChangePassword)}"
		>
			<div class="profile__user__contain">
				<div class="profile__user__field">
					<p class="profile__user__label xl">Mật khẩu hiện tại</p>
					<MInput name="OldPassword" />
				</div>
				<div class="profile__user__field">
					<p class="profile__user__label xl">Mật khẩu mới</p>
					<MInput name="NewPassword" />
				</div>
				<div class="profile__user__field">
					<p class="profile__user__label xl">Xác nhận mật khẩu</p>

					<MInput name="ConfirmPassword" />
				</div>

				<button class="btn profile__user__action xl" type="submit">
					Xác nhận
				</button>
			</div>
		</form>
	</div>
</template>

<style></style>
