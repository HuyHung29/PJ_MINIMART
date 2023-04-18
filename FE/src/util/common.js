import RESOURCES from "../constants/resource";
/**
 * @param {Date} dateTime Ngày tháng còn format
 * Hàm định dạng ngày tháng
 * @returns Ngày tháng dạng DD/MM/YYYY
 * Author: LHH - 02/01/23
 */
export const formatDate = (dateTime) => {
	if (dateTime !== null && dateTime !== undefined && dateTime) {
		dateTime = new Date(dateTime);
		let date =
			dateTime.getDate() < 10
				? "0" + dateTime.getDate()
				: dateTime.getDate();
		let month =
			dateTime.getMonth() < 9
				? `0${dateTime.getMonth() + 1}`
				: dateTime.getMonth() + 1;
		let year = dateTime.getFullYear();

		return `${date}/${month}/${year}`;
	}

	return "";
};

/**
 * Hàm định dạng tiền tệ
 * @param {String} money Tiền cần định dạng
 * @returns Tiền dã được định dạng
 * Author: LHH - 02/01/23
 */
export const formatMoney = (money) => {
	if (Number.parseInt(money)) {
		money = money.toLocaleString("it-IT", {
			style: "currency",
			currency: "VND",
		});
		return money;
	}

	return 0 + " VND";
};

/**
 * Hàm chuyển đổi chuỗi thành ngày tháng
 * @param {String} data Ngày tháng cần convert
 * @returns Ngày tháng tương ứng
 * Author: LHH - 06/01/23
 */
export const convertStringToDate = (data) => {
	if (data) {
		const str = data;

		const [day, month, year] = str.split("/");
		if (day && month && year) {
			const date = new Date(+year, +month - 1, +day);
			return date;
		}

		return "";
	}

	return "";
};

/**
 * Hàm format
 * @param {date} date Ngày tháng cần convert
 * @returns Ngày tháng tương ứng
 * Author: LHH - 06/01/23
 */
export const convertStringToDateUSUK = (date, isJson) => {
	if (date) {
		const [day, month, year] = date.toString().split("/");

		const newDateString = `${year}/${month}/${day}`;

		const dtFormat = new Intl.DateTimeFormat("en-US", {
			day: "2-digit",
			month: "2-digit",
			year: "numeric",
		});

		const newDateTime = new Date(newDateString);
		if (isJson) {
			newDateTime.setHours(newDateTime.getHours() + 24);
		}

		return dtFormat.format(newDateTime);
	}
};

/**
 * Kiểm tra phần tử có quá giới hạn hay ko
 * Author: LHH - 31/01/23
 */
export const isOverflow = (element) => {
	if (element) {
		var curOverflow = element.style.overflow;

		if (!curOverflow || curOverflow === "visible")
			element.style.overflow = "hidden";

		var isOverflowing =
			element.clientWidth < element.scrollWidth ||
			element.clientHeight < element.scrollHeight;

		element.style.overflow = curOverflow;

		return isOverflowing;
	}

	return false;
};
