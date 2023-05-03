import { useToast } from "vue-toastification";
import addressApi from "@/api/addressAPI";
import RESOURCE from "@/constants/resource";

const toast = useToast();

const state = {
	address: [],
	pagination: {
		PageSize: 20,
		PageNumber: 1,
	},
	currentItem: {},
};

const getters = {};

const actions = {
	fetchAddress: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await addressApi.get(data);
			} else {
				payload = await addressApi.get(state.pagination);
			}

			commit("setAddress", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	getAddress: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await addressApi.get({ AddressId: data });

			let cate = payload.Data[0];

			commit("setCurrentItem", cate);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			commit("ui/hideLoading", null, { root: true });
		}
	},
	insert: async ({ commit, state }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await addressApi.create(data);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("insertAddress", Data);
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
	update: async ({ commit, state }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });

			data = { ...data, AddressId: state.currentItem.AddressId };
			commit("ui/showLoading", null, { root: true });

			const payload = await addressApi.update(
				state.currentItem.AddressId,
				data
			);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("replaceCate", Data);
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
	remove: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			console.log(data);
			const payload = await addressApi.delete(data);

			const { Message } = payload;

			const cate = await addressApi.get(state.pagination);

			commit("setAddress", cate);

			toast.success(Message);

			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
		}
	},
};

const mutations = {
	setAddress: (state, payload) => {
		const { Data, ...rest } = payload;

		state.address = [...Data];
		state.pagination = { ...rest };
	},
	insertAddress: (state, newCate) => {
		state.address.unshift(newCate);
	},
	replaceCate: (state, newCate) => {
		state.address = state.address.map((cate) => {
			if (cate.AddressId === newCate.AddressId) {
				return newCate;
			}

			return cate;
		});
	},
	setCurrentItem: (state, cate) => {
		state.currentItem = {
			...cate,
		};
	},
	clearCurrentItem: (state) => {
		state.currentItem = {};
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
