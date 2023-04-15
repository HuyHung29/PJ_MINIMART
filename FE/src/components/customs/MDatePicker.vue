<script setup>
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { onUpdated, ref, watch } from "vue";
import RESOURCES from "../../constants/resource";
import {
	convertStringToDate,
	inputValidation,
	isOverflow,
} from "../../util/common";

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
	value: {
		type: [String, Date],
		default: "",
	},
	rules: {
		type: Array,
		default: [],
	},
	tabindex: {
		type: Number,
	},
	placeholder: {
		type: String,
		default: "DD/MM/YYYY",
	},
	size: {
		type: String,
		validator(value) {
			return ["sm", "lg"].includes(value);
		},
	},
	error: {
		type: String,
	},
	isShowError: Boolean,
});

/**
 * Định nghĩa các emit
 * Author: LHH -
 */
const emit = defineEmits(["change", "error"]);

/**
 * Tạo các state của component
 * Author: LHH -
 */
const date = ref(null);
const datePickerRef = ref(null);
const inputVal = ref(null);
const inputRef = ref(null);
const errorRef = ref(null);
const isShowTooltip = ref(false);
const isError = ref(props.isShowError);

/**
 * Hàm format date
 * Author: LHH - 05/01/23
 */
const format = (date) => {
	try {
		if (date) {
			const day =
				date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
			const month =
				date.getMonth() + 1 < 10
					? "0" + (date.getMonth() + 1)
					: date.getMonth() + 1;
			const year = date.getFullYear();

			return `${day}/${month}/${year}`;
		}
		return "";
	} catch (error) {
		console.log(error);
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
 * Hàm xử lý validate
 * Author: LHH - 26/01/23
 */
const handleValidate = () => {
	const message = inputValidation(props.rules, date.value, props.name);

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
 * Hàm xử lý thay đổi input
 * Author: LHH - 16/01/23
 */
const handleInput = (e) => {
	try {
		const value = e.target.value;

		const dateRegex =
			/^([1-9]|[0-2][0-9]|3[0-1])\/([1-9]|0[1-9]|1[0-2])\/[1-2][0-9][0-9][0-9]$/;
		if (dateRegex.test(value)) {
			date.value = convertStringToDate(value);
			emit("change", { name: props.name, value: date.value });
		}

		inputVal.value = value;
	} catch (error) {
		console.log(error);
	}
};

/**
 * Hàm xử lý thay đổi date
 * Author: LHH - 16/01/23
 */
const handleChangeDate = () => {
	const dateValue = convertStringToDate(inputRef.value.value);

	if (date.value !== dateValue) {
		if (dateValue) {
			date.value = new Date(dateValue);
		} else {
			date.value = null;
			inputVal.value = "";
			inputRef.value.value = null;
		}
	}
};

/**
 * Hàm xử lý khi tab ra khỏi ô input
 * @param {Event} e
 * Author: LHH -24/02/23
 */
const handleChangeDateOnKeyEvent = (e) => {
	if (e.keyCode === RESOURCES.KEYCODE.TAB) {
		handleChangeDate();
		datePickerRef.value.closeMenu();
	}
};

/**
 * Hàm xử lý ẩn các ngày quá ngày hiện tại
 * @param {Date} time
 * Author: LHH - 24/02/23
 */
const disabledDate = (time) => {
	return time.getTime() > Date.now();
};

/**
 * Theo dõi sự thay đổi của các state
 * Author: LHH - 05/01/23
 */
watch(date, () => {
	try {
		emit("change", { name: props.name, value: date.value });
	} catch (error) {
		console.log(error);
	}
});

/**
 * Theo dõi sự thay đổi của props
 * Author: LHH - 05/01/23
 */
watch(
	() => props.value,
	(newVal) => {
		try {
			date.value = newVal;
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
 * Xử lý show error tooltip
 * Author: LHH - 31/01/23
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
		class="date-picker"
		:class="{
			'date-picker--sm': size === 'sm',
			'date-picker--lg': size === 'lg',
			invalid: error,
		}"
	>
		<label v-if="title" class="date-picker__label">{{ title }}</label>
		<Datepicker
			v-model="date"
			placeholder="DD/MM/YYYY"
			text-input
			auto-apply
			close-on-scroll
			show-now-button
			locale="vi"
			:disabled-dates="disabledDate"
			:enable-time-picker="false"
			:format="format"
			hide-offset-dates
			:day-names="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"
			ref="datePickerRef"
		>
			<template #dp-input="{ value }">
				<div class="date-picker__wrapper">
					<input
						type="text"
						class="date-picker__input"
						:placeholder="placeholder"
						:name="name"
						:value="value"
						:tabindex="tabindex"
						autocomplete="off"
						@input="handleInput"
						@blur="handleChangeDate"
						@keydown="handleChangeDateOnKeyEvent"
						ref="inputRef"
					/>
					<p class="date-picker__icon">
						<i></i>
					</p>
				</div>
			</template>
			<template #month="{ text, value }">
				{{ text }}
			</template>
			<template #now-button="{ selectCurrentDate }">
				<div class="date-picker__calendar__footer">
					<button class="btn btn--link" @click="selectCurrentDate()">
						<a class="btn__text">Hôm nay</a>
					</button>
				</div>
			</template>
		</Datepicker>
		<p v-show="error || isError" class="date-picker__error" ref="errorRef">
			{{ error || "Thông tin không hợp lệ" }}
		</p>
		<p v-if="isShowTooltip" class="date-picker__error__tooltip">
			{{ error }}
		</p>
	</div>
</template>
