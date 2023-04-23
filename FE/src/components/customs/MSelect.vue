<script setup>
import useClickOutSide from "@/util/clickOutSide";
import { onMounted, reactive, ref, watch } from "vue";
import RESOURCES from "@/constants/resource";

/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	options: {
		type: Array,
		required: true,
		default: [],
	},
	title: {
		type: String,
	},
	name: {
		type: String,
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
	readonly: {
		type: Boolean,
		default: false,
	},
	initialValue: [String, Number],
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
	listSearch: [...props.options],
});

/**
 * Định nghĩa các ref
 * Author: LHH - 12/01/23
 */
const listRef = ref(null);
const inputRef = ref(null);
const errorRef = ref("null");
const isError = ref(false);

/**
 * Xử lý thay đổi dữ liệu
 * Author: LHH - 04/01/23
 */
const handleChangeValue = (item) => {
	try {
		state.value = item.title;
		state.isShow = false;
		emit("select", item.value);
	} catch (error) {
		console.log(error);
	}
};

onMounted(() => {
	if (props.initialValue) {
		emit("select", props.initialValue);
	}
});

/**
 * Theo dõi sự thay đổi của props
 * Author: LHH - 04/01/23
 */
watch(
	() => props,
	() => {
		try {
			state.value = props.defaultValue;
			state.listSearch = [...props.options];
		} catch (error) {
			console.log(error);
		}
	},
	{ deep: true }
);

watch(
	() => props.initialValue,
	() => {
		if (props.initialValue) {
			emit("select", props.initialValue);
		}
	},
	{ deep: true }
);

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
		state.listSearch = props.options;
		emit("select", "");

		state.value = "";
	}

	state.listSearch = props.options.filter((item) =>
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

			state.indexItem = props.options.findIndex(
				(item) => item.value === state.listSearch[state.indexItem].value
			);
			state.listSearch = [...props.options];
			emit("select", state.listSearch[state.indexItem].value);

			break;
		case TAB:
			state.isShow = false;
			break;
		default:
			break;
	}
};
</script>

<template>
	<div
		class="c-select"
		:class="{
			'c-select--sm': size === 'sm',
			'c-select--lg': size === 'lg',
			invalid: error,
		}"
		ref="listRef"
	>
		<label v-if="title" for="" class="c-select__label"
			>{{ title }} <span v-if="isRequired">*</span></label
		>
		<div class="c-select__input__wrap">
			<button
				type="button"
				class="c-select__icon"
				@click="state.isShow = !state.isShow"
			>
				<i></i>
			</button>
			<input
				type="text"
				class="c-select__input"
				:readonly="readonly"
				v-model="state.value"
				@input="handleInput"
				@keydown="handleChangeItemSelected"
				@focus="handleFocus"
				ref="inputRef"
			/>
			<ul class="c-select__list" v-show="state.isShow" :style="style">
				<li
					v-for="(item, index) in state.listSearch"
					class="c-select__item"
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
			class="c-select__error"
			ref="errorRef"
		>
			{{ error }}
		</p>
	</div>
</template>

<style></style>
