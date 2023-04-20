<script setup>
import CheckBox from "@/components/customs/MCheckBox.vue";
import { formatDate } from "@/util/common";
import { inject, ref } from "vue";
// import RESOURCES from "../constants/resource";
// import useDetectOutsideClick from "../composable/clickOutSide";
/**
 * Định nghĩa các props
 * Author: LHH - 04/01/23
 */
const props = defineProps({
	category: {
		type: Object,
		required: true,
		default: {},
	},
	checkList: {
		type: Array,
		default: [],
	},
});

/**
 * Định nghĩa các ref của component
 * Author: LHH - 04/01/23
 */
const moreBtnRef = ref(null);
const nameRef = ref(null);
const positionRef = ref(null);
const bankNameRef = ref(null);
const bankBranchRef = ref(null);

/**
 * Định nghĩa các emiit
 * Author: LHH - 04/01/23
 */
const emit = defineEmits(["click", "check", "closeList"]);

/**
 * Xử lý khi ấn nút option
 * Author: LHH - 04/01/23
 */
// const onOptionBtnClick = () => {
// 	try {
// 		const position = moreBtnRef.value.getBoundingClientRect();
// 		emit("click", {
// 			employeeId: props.category.CategoryId,
// 			employeeCode: props.employee.EmployeeCode,
// 			top: position.top,
// 			right: document.body.offsetWidth - position.left,
// 			width: position.width,
// 			height: position.height,
// 		});
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Xử lý khi ấn nút sửa
 * Author: LHH - 04/01/23
 */
// const onUpdateBtnClick = () => {
// 	try {
// 		handleOpenForm(RESOURCES.FORM_MODE.EDIT, props.category.CategoryId);
// 	} catch (error) {
// 		console.log(error);
// 	}
// };

/**
 * Xử lý check item
 * Author: LHH - 04/01/23
//  */
// const handleCheckBox = (data) => {
// 	try {
// 		emit("check", data.value);
// 	} catch (error) {
// 		console.log(error);
// 	}
// };
</script>

<template>
	<tr class="c-table__row" @dblclick="onUpdateBtnClick()">
		<td class="c-table__col text-center">
			<CheckBox
				name="c-table-checkbox"
				:id="category.CategoryId"
				:value="category.CategoryId"
				@check="handleCheckBox"
				:checked="checkList.includes(category.CategoryId)"
			/>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text" ref="nameRef">
				{{ category.Thumbnail }}
			</p>
		</td>
		<td class="c-table__col">
			<p class="c-table__col__text">{{ category.CategoryName }}</p>
		</td>
		<td class="c-table__col text-center">
			<button class="c-table__action">
				<p class="c-table__action__update" @click="onUpdateBtnClick">
					Sửa
				</p>
				<button
					class="c-table__action__more"
					@click="onOptionBtnClick"
					ref="moreBtnRef"
					@blur="emit('closeList')"
				>
					<i class="c-table__action__icon"></i>
				</button>
			</button>
		</td>
	</tr>
</template>
