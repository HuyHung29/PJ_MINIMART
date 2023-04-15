<script setup>
/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	id: {
		type: String,
		required: true,
	},
	title: {
		type: String,
		default: "",
	},
	name: {
		type: String,
		required: true,
	},
	value: {
		type: String,
		default: "",
	},
	checked: {
		type: Boolean,
		default: false,
	},
});

/**
 * Định nghĩa các emit
 * Author: LHH - 04/01/23
 */
const emit = defineEmits(["check"]);

/**
 * Xử lý sự kiện checked của checkbox
 * Author: LHH - 04/01/23
 */
const handleCheck = (e) => {
	try {
		emit("check", e.target);
	} catch (error) {
		console.log(error);
	}
};
</script>

<template>
	<div class="checkbox">
		<input
			@change="handleCheck"
			type="checkbox"
			:name="name"
			:id="id"
			class="checkbox__input"
			:checked="checked"
			:value="value"
		/>
		<label :for="id" class="checkbox__label"><i></i></label>
		<label v-if="title" :for="id" class="checkbox__title">{{
			title
		}}</label>
	</div>
</template>
