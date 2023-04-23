<script setup>
import { toRef } from "vue";
import { useField } from "vee-validate";

const props = defineProps({
	type: {
		type: String,
		default: "text",
	},
	value: {
		type: String,
		default: "",
	},
	name: {
		type: String,
		required: true,
	},
	label: {
		type: String,
	},
	successMessage: {
		type: String,
		default: "",
	},
	placeholder: {
		type: String,
		default: "",
	},
	readonly: {
		type: Boolean,
		default: false,
	},
	isRequired: Boolean,
});

const name = toRef(props, "name");

const {
	value: inputValue,
	errorMessage,
	handleBlur,
	handleChange,
	meta,
} = useField(name, undefined, {
	initialValue: props.value,
});
</script>

<template>
	<div
		class="textfield"
		:class="{
			invalid: errorMessage,
		}"
	>
		<label v-if="label" class="textfield__label"
			>{{ label }} <span v-if="isRequired">*</span>
		</label>
		<div class="textfield__input__wrap">
			<input
				:type="type"
				class="textfield__input"
				:name="name"
				:id="name"
				:placeholder="placeholder"
				:value="inputValue"
				@input="handleChange"
				@blur="handleBlur"
				:readonly="readonly"
			/>
		</div>
		<p v-show="errorMessage" class="textfield__error" ref="errorRef">
			{{ errorMessage }}
		</p>
	</div>
</template>
