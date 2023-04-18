<script setup></script>

<template>
	<div class="user__purchase">
		<div class="user__purchase__header">
			<ul class="user__purchase__header__tab">
				<li class="user__purchase__header__item">
					<p
						class="user__purchase__header__link"
						:class="{
							active: 0 === 0,
						}"
					>
						Tất cả
					</p>
				</li>
				<li class="user__purchase__header__item">
					<p
						class="user__purchase__header__link"
						:class="{
							active: 0 === 1,
						}"
					>
						Chờ xác nhận
					</p>
				</li>
				<li class="user__purchase__header__item">
					<p
						class="user__purchase__header__link"
						:class="{
							active: 0 === 1,
						}"
					>
						Đã vận chuyển
					</p>
				</li>
				<li class="user__purchase__header__item">
					<p
						class="user__purchase__header__link"
						:class="{
							active: 0 === 1,
						}"
					>
						Đã giao
					</p>
				</li>
				<li class="user__purchase__header__item">
					<p
						class="user__purchase__header__link"
						:class="{
							active: 0 === 1,
						}"
					>
						Hủy bỏ
					</p>
				</li>

				<div
					class="user__purchase__header__line"
					style="{active.style}"
				></div>
			</ul>
		</div>

		<div class="user__purchase__list">
			<div class="user__purchase--empty" v-if="false">
				<img src="@/assets/images/list-empty.png" alt="anh" />
				<p>Không có đơn hàng nào</p>
			</div>
			<div class="user__purchase__item" key="{index}" v-else>
				<p
					:class="{
						user__purchase__item__status: true,
						_0: 0 === 'Chờ xác nhận',
						_1: 0 === 'Chờ lấy hàng',
						_2: 0 === 'Đã thanh toán',
					}"
				>
					{0}
				</p>
				<div class="user__purchase__product">
					<div class="user__purchase__product__item" key="{ind}">
						<img
							src="{product.pictures[0]}"
							alt="anh"
							class="user__purchase__product__item__thumbnail"
						/>
						<div class="user__purchase__product__item__info">
							<p class="user__purchase__product__item__name">
								{product.title}
							</p>
							<p class="user__purchase__product__item__quantity">
								x{order.quantity[ind]}
							</p>
						</div>
						<p class="user__purchase__product__item__total">
							{productPrice.toLocaleString()}
							<sup>đ</sup>
						</p>
					</div>
				</div>
				<div class="user__purchase__item__action">
					<p class="user__purchase__item__sum">
						Tổng số tiền:
						<span>
							{total.toLocaleString()}
							<sup>đ</sup>
						</span>
					</p>
				</div>
			</div>
		</div>
	</div>
</template>

<style></style>
