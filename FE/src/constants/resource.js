const RESOURCE = {
	URL: {
		AUTH: "auth/",
		ADDRESS: "address/",
		CATEGORY: "category/",
		CART: "cart/",
		COUPON: "coupon/",
		INVOICE: "invoice/",
		NEWS: "news/",
		ORDER: "order/",
		PRODUCT: "product/",
		SUPPLIER: "supplier/",
		USER: "user/",
	},
	KEYCODE: {
		ARROW_DOWN: 38,
		ARROW_UP: 40,
		ENTER: 13,
		TAB: 9,
		NUMBER_ONE: 49,
	},
	PAGINATION: [
		{
			value: 20,
			title: "20 bản ghi trên 1 trang",
		},
		{
			value: 50,
			title: "50 bản ghi trên 1 trang",
		},
		{
			value: 100,
			title: "100 bản ghi trên 1 trang",
		},
		{
			value: 150,
			title: "150 bản ghi trên 1 trang",
		},
	],
	HELPTEXT: "Đã có lỗi xảy ra vui lòng thử lại sau",
	MODAL_TITLE: {
		INFO: "Xác nhận",
		WARNING: "Xác nhận xóa",
		ERROR: "Dữ liệu không đúng",
	},
	MODAL_TYPE: {
		INFO: "INFO",
		WARNING: "WARNING",
		ERROR: "ERROR",
	},
	MODAL_MESSAGE: {
		INFO: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
		WARNING: "Bạn có thực sự muốn xóa bản ghi này không?",
		WARNING_MULTIPLE:
			"Bạn có thực sự muốn xóa những bản ghi được chọn không?",
	},
	UNIT: [
		{
			value: 0,
			title: "Hộp",
		},
		{
			value: 1,
			title: "Túi",
		},
		{
			value: 2,
			title: "Chai",
		},
	],
	VOLUME: [
		{
			value: 0,
			title: "Gam",
		},
		{
			value: 1,
			title: "Kg",
		},
		{
			value: 2,
			title: "L",
		},
		{
			value: 3,
			title: "mL",
		},
	],
	ORDER_STATUS: [],
};

export default RESOURCE;
