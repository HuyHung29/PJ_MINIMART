<script setup>
import { onMounted, onUpdated, ref, watch } from "vue";
import { inputValidation, isOverflow } from "../../util/common";
/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	title: {
		type: String,
		default: "",
	},
	name: {
		type: String,
		required: true,
	},
	value: {
		type: [String, Number],
		default: "",
	},
	rules: {
		type: Array,
		default: [],
	},
	placeholder: {
		type: String,
		default: "",
	},
	tooltip: {
		type: String,
	},
	tabindex: {
		type: Number,
	},
	type: {
		type: String,
		default: "text",
	},
	size: {
		type: String,
		validator(value) {
			return ["sm", "lg"].includes(value);
		},
	},
	isRequired: {
		type: Boolean,
		default: false,
	},
	error: {
		type: String,
		default: null,
	},
	isShowError: Boolean,
});

/**
 * Định nghĩa các emit
 * Author: LHH - 04/01/23
 */
const emit = defineEmits(["change", "error"]);

/**
 * Định nghĩa state dùng trong component
 * Author: LHH - 04/01/23
 */
const inputState = ref(props.value);
const inputRef = ref(null);
const errorRef = ref("null");
const isShowTooltip = ref(false);
const isError = ref(props.isShowError);

/**
 * Hàm set focus cho input
 * Author: LHH - 26/01/23
 */
const setFocusInput = () => {
	try {
		if (inputRef.value) {
			inputRef.value.focus();
		}
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý validate
 * Author: LHH - 26/01/23
 */
const handleValidate = () => {
	const message = inputValidation(
		props.rules,
		inputState.value.trim() || props.value.trim(),
		props.name
	);

	emit("error", {
		name: props.name,
		message,
	});
};

/**
 * Định nghĩa các expose
 * Author: LHH - 26/01/23
 */
defineExpose({
	setFocusInput,
	handleValidate,
});

/**
 * Xử lý binding dữ liệu
 * Author: LHH - 04/01/23
 */
watch(inputState, () => {
	try {
		emit("change", {
			name: props.name,
			value: inputState.value,
		});
	} catch (error) {
		console.log(error);
	}
});

/**
 * Xử lý binding dữ liệu
 * Author: LHH - 04/01/23
 */
watch(
	() => props.value,
	() => {
		try {
			inputState.value = props.value;
		} catch (error) {
			console.log(error);
		}
	}
);

/**
 * Xử lý hiển thị lỗi
 * Author: LHH - 21/02/23
 */
watch(
	() => props.isShowError,
	() => {
		isError.value = props.isShowError;
	}
);

/**
 * Xử lý hiên tooltip
 * Author: LHH - 04/01/23
 */
onUpdated(() => {
	if (isOverflow(errorRef.value)) {
		isShowTooltip.value = true;
	} else {
		isShowTooltip.value = false;
	}
});
</script>

<template>
	<div
		class="textfield"
		:class="{
			'textfield--sm': size === 'sm',
			'textfield--lg': size === 'lg',
			invalid: error,
		}"
	>
		<label v-if="title" class="textfield__label"
			>{{ title }} <span v-if="isRequired">*</span>
			<p v-if="tooltip" class="textfield__tooltip">
				{{ tooltip }}
			</p>
		</label>
		<div class="textfield__input__wrap">
			<input
				ref="inputRef"
				:type="type"
				class="textfield__input"
				:name="name"
				:placeholder="placeholder"
				:tabindex="tabindex"
				:value="value"
				v-model="inputState"
			/>
		</div>
		<p v-show="error || isError" class="textfield__error" ref="errorRef">
			{{ error || "Thông tin không hợp lệ" }}
		</p>

		<p v-if="isShowTooltip" class="textfield__error__tooltip">
			{{ error }}
		</p>
	</div>
</template>
