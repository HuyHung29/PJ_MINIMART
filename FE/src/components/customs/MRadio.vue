<script setup>
import { ref } from "vue";
import RESOURCES from "../../constants/resource";

/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	title: {
		type: String,
	},
	name: {
		type: String,
		required: true,
	},
	id: {
		type: String,
	},
	list: {
		type: Array,
		required: true,
	},
	defaultValue: {
		type: [Number, String],
		default: 0,
	},
	tabindex: Number,
	size: {
		type: String,
		validator(value) {
			return ["sm", "lg"].includes(value);
		},
	},
	error: {
		type: String,
		default: "",
	},
});

/**
 * Định nghĩa các emit
 * Author: LHH - 04/01/23
 */
const emit = defineEmits(["check"]);

/**
 * Định nghĩa các state của component
 * Author: LHH - 04/01/23
 */
const radioValue = ref(props.defaultValue);

/**
 * Xử lý binding dữ liệu
 * Author: LHH - 04/01/23
 */
const handleRadioChange = () => {
	try {
		emit("check", { name: props.name, value: radioValue.value });
	} catch (error) {
		console.log(error);
	}
};

/**
 * Xử lý check radio
 * Author: LHH - 04/01/23
 */
const handleCheck = (e, item) => {
	if (document.activeElement === e.target) {
		if (e.keyCode === RESOURCES.KEYCODE.ENTER) {
			emit("check", { name: props.name, value: item.value });
		}
	}
};
</script>

<template>
	<div
		class="radiofield radiofield--sm"
		:class="{
			'radiofield--sm': size === 'sm',
			'radiofield--lg': size === 'lg',
		}"
	>
		<label for="" class="radiofield__label">{{ title }}</label>
		<div class="radiofield__input__wrap">
			<div
				class="radiofield__input__item"
				v-for="item in list"
				:key="item.title"
			>
				<input
					type="radio"
					:name="name"
					:id="item.title"
					class="radiofield__input"
					:value="item.value"
					:checked="defaultValue === item.value"
					v-model="radioValue"
					@change="handleRadioChange"
				/>
				<label
					:for="item.title"
					class="radiofield__input__icon"
					:tabindex="tabindex"
					@keydown="handleCheck($event, item)"
				></label>
				<label :for="item.title" class="radiofield__input__label">{{
					item.title
				}}</label>
			</div>
		</div>
		<p v-show="error" class="radiofield__error">{{ error }}</p>
	</div>
</template>
