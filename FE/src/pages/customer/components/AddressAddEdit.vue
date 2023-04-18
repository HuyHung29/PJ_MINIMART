<script setup>
import Col from "@/components/bootstrap/Col.vue";
import Row from "@/components/bootstrap/Row.vue";
import MInput from "@/components/customs/MInput.vue";
</script>

<template>
	<div class="profile__address">
		<form class="profile__address__form">
			<Row>
				<h2>Địa chỉ mới</h2>

				<Col md="12">
					<MInput name="City" label="Thành phố" />
				</Col>

				<Col md="12">
					<MInput name="Distric" label="Quận/Huyện" />
				</Col>

				<Col md="12">
					<MInput name="Village" label="Xã/Phường" />
				</Col>

				<Col md="12">
					<MInput name="Detail" label="Địa chỉ cụ thể" />
				</Col>

				<Col md="12" class="profile__address__form__action">
					<button class="btn btn-no-border" type="reset">
						Trở lại
					</button>
					<button class="btn btn-normal" type="submit">
						Hoàn thành
					</button>
				</Col>
			</Row>
		</form>
	</div>
</template>

<style scoped>
.row {
	row-gap: 20px;
}
</style>
