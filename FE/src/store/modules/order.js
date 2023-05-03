import { useToast } from "vue-toastification";
import orderApi from "@/api/orderAPI";
import RESOURCE from "@/constants/resource";

const toast = useToast();

const state = {
	order: [],
	pagination: {
		PageSize: 20,
		PageNumber: 1,
		TotalPage: 1,
		TotalRecord: 0,
		Filter: "",
	},
};

const getters = {};

const actions = {
	fetchOrder: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await orderApi.get(data);
			} else {
				payload = await orderApi.get(state.pagination);
			}

			commit("setOrder", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	fetchOrderUser: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await orderApi.getByAccount(data);
			} else {
				payload = await orderApi.getByAccount(state.pagination);
			}

			commit("setOrder", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	insert: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await orderApi.create(data);

			const { Message } = payload;

			toast.success(Message);

			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			callback.setErrors(MoreInfo || Error.MoreInfo);
			return false;
		}
	},
	changeStatus: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await orderApi.update(data);

			const { Message } = payload;

			toast.success(Message);
			commit("changeStatus", data);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			callback.setErrors(MoreInfo || Error.MoreInfo);
			return false;
		}
	},
};

const mutations = {
	setOrder: (state, payload) => {
		const { Data, ...rest } = payload;

		state.order = [...Data];
		state.pagination = { ...rest };
	},
	changeStatus: (state, data) => {
		let statusName = "";

		switch (data.Status) {
			case RESOURCE.ORDER_STATUS.Waiting:
				statusName = "Chờ xác nhận";
				break;
			case RESOURCE.ORDER_STATUS.Delivered:
				statusName = "Đang vận chuyển";
				break;
			case RESOURCE.ORDER_STATUS.Cancled:
				statusName = "Đã hủy";
				break;
			case RESOURCE.ORDER_STATUS.Done:
				statusName = "Đã giao";
				break;
			default:
				statusName = "";
				break;
		}
		state.order = state.order.map((item) => {
			if (item.OrderId === data.OrderId) {
				return { ...item, Status: data.Status, StatusName: statusName };
			}

			return item;
		});
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
