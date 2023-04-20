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
	employee: {
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
// 			employeeId: props.employee.EmployeeId,
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
// 		handleOpenForm(RESOURCES.FORM_MODE.EDIT, props.employee.EmployeeId);
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
				:id="employee.EmployeeId"
				:value="employee.EmployeeId"
				@check="handleCheckBox"
				:checked="checkList.includes(employee.EmployeeId)"
			/>
		</td>
		<td class="c-table__col w-150">
			<p class="c-table__col__text">{{ employee.EmployeeCode }}</p>
		</td>
		<td
			class="c-table__col w-250"
			:class="{
				overflow: isOverflow(nameRef),
			}"
		>
			<p class="c-table__col__text" ref="nameRef">
				{{ employee.FullName }}
			</p>
			<p class="c-table__col__tooltip">{{ employee.FullName }}</p>
		</td>
		<td class="c-table__col w-100">
			<p class="c-table__col__text">{{ employee.GenderName }}</p>
		</td>
		<td class="c-table__col text-center w-200">
			<p class="c-table__col__text">
				{{ formatDate(employee.DateOfBirth) }}
			</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__text">{{ employee.IdentityNumber }}</p>
		</td>
		<td
			class="c-table__col w-200"
			:class="{
				overflow: isOverflow(positionRef),
			}"
		>
			<p class="c-table__col__text" ref="positionRef">
				{{ employee.Position }}
			</p>
			<p class="c-table__col__tooltip">{{ employee.Position }}</p>
		</td>
		<td class="c-table__col w-300">
			<p class="c-table__col__text">{{ employee.DepartmentName }}</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__text">{{ employee.PhoneNumber }}</p>
		</td>
		<td class="c-table__col w-200">
			<p class="c-table__col__text">{{ employee.BankAccount }}</p>
		</td>
		<td
			class="c-table__col w-200"
			:class="{
				overflow: isOverflow(bankNameRef),
			}"
		>
			<p class="c-table__col__text" ref="bankNameRef">
				{{ employee.BankName }}
			</p>
			<p class="c-table__col__tooltip">{{ employee.BankName }}</p>
		</td>
		<td
			class="c-table__col w-200"
			:class="{
				overflow: isOverflow(bankBranchRef),
			}"
		>
			<p class="c-table__col__text" ref="bankBranchRef">
				{{ employee.BankBranch }}
			</p>
			<p class="c-table__col__tooltip">{{ employee.BankBranch }}</p>
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
