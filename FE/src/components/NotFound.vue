<script setup></script>

<template>
	<div className="not-found">
		<div className="mars"></div>
		<img
			src="https://assets.codepen.io/1538474/404.svg"
			className="logo-404"
			alt="img"
		/>
		<img
			src="https://assets.codepen.io/1538474/meteor.svg"
			className="meteor"
			alt="img"
		/>
		<p className="title mb-5">Oh no!!</p>
		<p className="subtitle">
			Đường dẫn bạn truy cập không tồn tại <br />
			Quay lại trang chủ
		</p>
		<div align="center">
			<router-link to="/" className="btn-back">
				Trở lại trang chủ
			</router-link>
		</div>
		<img
			src="https://assets.codepen.io/1538474/astronaut.svg"
			className="astronaut"
			alt="img"
		/>
		<img
			src="https://assets.codepen.io/1538474/spaceship.svg"
			className="spaceship"
			alt="img"
		/>
	</div>
</template>

<style></style>
