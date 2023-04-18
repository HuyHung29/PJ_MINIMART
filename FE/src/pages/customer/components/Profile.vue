<script setup>
import MInput from "@/components/customs/MInput.vue";
</script>

<template>
	<div class="profile">
		<div class="profile__header">
			<h2 class="profile__heading">Hồ sơ của tôi</h2>
			<p class="profile__sub-heading mt-4">
				Quản lý thông tin hồ sơ để bảo mật tài khoản
			</p>
		</div>
		<form
			class="profile__user"
			id="profile-form"
			onSubmit="{handleSubmit(onUserChangeInfo)}"
		>
			<div class="profile__user__field">
				<p class="profile__user__label">Tên đăng nhập</p>
				<p class="profile__user__data">huyhung29</p>
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Họ tên</p>
				<!-- <Controller name='surname' control={control} render={({ field })
				=> (
				<InputField
					{...field}
					errors="{errors}"
					label=""
					placeholder="Họ"
					ref="{null}"
					class="profile__user__input"
				/>
				)} /> -->
				<MInput name="FullName" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Giới tính</p>
				<!-- <Controller name='name' control={control} render={({ field }) =>
				(
				<InputField
					{...field}
					errors="{errors}"
					label=""
					placeholder="Tên"
					ref="{null}"
					class="profile__user__input"
				/>
				)} /> -->
				<MInput name="FullName" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Số điện thoại</p>
				<!-- <Controller
						name='phone'
						control={control}
						render={({ field }) => (
							<InputField
								{...field}
								errors={errors}
								label=''
								placeholder='Số điện thoại'
								ref={null}
								class='profile__user__input'
							/>
						)}
					/> -->
				<MInput name="PhoneNumber" />
			</div>
			<div class="profile__user__field">
				<p class="profile__user__label">Email</p>
				<!-- <Controller
						name='email'
						control={control}
						render={({ field }) => (
							<InputField
								{...field}
								errors={errors}
								label=''
								placeholder='Email'
								ref={null}
								class='profile__user__input'
							/>
						)}
					/> -->
				<MInput name="Email" />
			</div>
			<button class="btn profile__user__action" type="submit">Lưu</button>
		</form>
	</div>
</template>

<style>
.profile__user__action {
	color: white;
}
</style>
