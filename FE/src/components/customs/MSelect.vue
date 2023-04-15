<script setup>
import useClickOutSide from "../../composable/clickOutSide";
import { onUpdated, reactive, ref, watch } from "vue";
import RESOURCES from "../../constants/resource";
import { inputValidation, isOverflow } from "../../util/common";

/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	listValue: {
		type: Array,
		required: true,
		default: [],
	},
	title: {
		type: String,
	},
	name: {
		type: String,
		required: true,
	},
	type: {
		type: String,
		validator(value) {
			return ["dropdown", "combobox"].includes(value);
		},
	},
	hasError: {
		type: Boolean,
		default: true,
	},
	tabindex: {
		type: Number,
	},
	defaultValue: {
		type: [String, Number],
	},
	rules: {
		type: Array,
		default: [],
	},
	size: {
		type: String,
		validator(value) {
			return ["sm", "lg"].includes(value);
		},
	},
	style: {
		type: Object,
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
const emit = defineEmits(["select", "error"]);

/**
 * Định nghĩa các state của component
 * Author: LHH - 04/01/23
 */
const state = reactive({
	isShow: false,
	value: props.defaultValue,
	indexItem: props.type === "dropdown" ? null : 0,
	listSearch: [...props.listValue],
});

/**
 * Định nghĩa các ref
 * Author: LHH - 12/01/23
 */
const listRef = ref(null);
const inputRef = ref(null);
const errorRef = ref("null");
const isShowTooltip = ref(false);
const isError = ref(false);

/**
 * Xử lý thay đổi dữ liệu
 * Author: LHH - 04/01/23
 */
const handleChangeValue = (item) => {
	try {
		state.value = item.title;
		state.isShow = false;
		emit("select", { name: props.name, value: item.value });
	} catch (error) {
		console.log(error);
	}
};

/**
 * Theo dõi sự thay đổi của props
 * Author: LHH - 04/01/23
 */
watch(
	() => props,
	() => {
		try {
			state.value = props.defaultValue;
			state.listSearch = [...props.listValue];
		} catch (error) {
			console.log(error);
		}
	},
	{ deep: true }
);

watch(
	() => props.isShowError,
	() => {
		isError.value = props.isShowError;
	}
);

/**
 * Xử lý show tooltip error
 * Author: LHH - 31/01/23
 */
onUpdated(() => {
	if (isOverflow(errorRef.value)) {
		isShowTooltip.value = true;
	} else {
		isShowTooltip.value = false;
	}
});

/**
 * Xử lý đóng list khi click ra ngoài
 * Author: LHH -
 */
useClickOutSide(listRef, () => {
	if (state.isShow) {
		state.isShow = false;
	}
});

/**
 * Xử lý tìm kiếm
 * Author: LHH - 12/01/23
 */
const handleInput = (e) => {
	state.isShow = true;
	state.indexItem = 0;
	const inputVal = e.target.value.toLowerCase();

	if (!inputVal) {
		state.indexItem = 0;
		state.listSearch = props.listValue;
		emit("select", {
			name: props.name,
			value: "",
		});

		state.value = "";
	}

	state.listSearch = props.listValue.filter((item) =>
		item.title.toLowerCase().includes(inputVal)
	);
};

/**
 * Xử lý ấn nút lên xuống và enter
 * Author: LHH - 12/01/23
 */

const handleChangeItemSelected = (e) => {
	const { ARROW_DOWN, ARROW_UP, ENTER, TAB } = RESOURCES.KEYCODE;
	switch (e.keyCode) {
		case ARROW_DOWN:
			state.isShow = true;
			if (state.indexItem > 0) {
				state.indexItem--;
			}
			break;
		case ARROW_UP:
			state.isShow = true;
			const length = state.listSearch.length;
			if (state.indexItem < length - 1) {
				state.indexItem++;
			} else {
				state.indexItem = 0;
			}
			break;
		case ENTER:
			state.isShow = false;
			state.value = state.listSearch[state.indexItem].title;

			state.indexItem = props.listValue.findIndex(
				(item) => item.value === state.listSearch[state.indexItem].value
			);
			state.listSearch = [...props.listValue];
			emit("select", {
				name: props.name,
				value: state.listSearch[state.indexItem].value,
			});

			break;
		case TAB:
			state.isShow = false;
			break;
		default:
			break;
	}
};

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
 * Xử lý hiện list khi focus vào input
 * Author: LHH - 25/02/23
 */
const handleFocus = () => {
	state.isShow = true;
};

/**
 * Hàm xử lý validate
 * Author: LHH - 26/01/23
 */
const handleValidate = () => {
	const message = inputValidation(props.rules, state.value, props.name);

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
</script>

<template>
	<div
		class="select"
		:class="{
			'select--sm': size === 'sm',
			'select--lg': size === 'lg',
			invalid: error,
		}"
		ref="listRef"
	>
		<label v-if="title" for="" class="select__label"
			>{{ title }} <span v-if="isRequired">*</span></label
		>
		<div class="select__input__wrap">
			<button class="select__icon" @click="state.isShow = !state.isShow">
				<i></i>
			</button>
			<input
				type="text"
				class="select__input"
				:readonly="type === 'dropdown'"
				v-model="state.value"
				:tabindex="tabindex"
				@input="handleInput"
				@keydown="handleChangeItemSelected"
				@focus="handleFocus"
				ref="inputRef"
			/>
			<ul class="select__list" v-show="state.isShow" :style="style">
				<li
					v-for="(item, index) in state.listSearch"
					class="select__item"
					:class="{
						selected: item.title === state.value,
						'on-index': state.indexItem === index,
					}"
					:key="item.value"
					@click="handleChangeValue(item)"
				>
					{{ item.title }}
				</li>
			</ul>
		</div>
		<p
			v-show="(hasError && error) || isError"
			class="select__error"
			ref="errorRef"
		>
			{{ error }}
		</p>
		<p v-if="!state.isShow && isShowTooltip" class="select__error__tooltip">
			{{ error }}
		</p>
	</div>
</template>

<style></style>
