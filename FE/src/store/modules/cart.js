import { useToast } from "vue-toastification";
import cartApi from "@/api/cartAPI";
import RESOURCE from "@/constants/resource";
import { generateFormData } from "@/util/common";

const toast = useToast();

const state = {
	cart: [],
	pagination: {
		PageNumber: 1,
		PageSize: 20,
		TotalPage: 1,
		TotalRecord: 0,
	},
	currentItem: {},
};

const getters = {};

const actions = {
	fetchCart: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await cartApi.get(data);
			} else {
				payload = await cartApi.get(state.pagination);
			}

			commit("setCart", payload);
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

			const payload = await cartApi.add(data);

			const { Message } = payload;

			toast.success(Message);

			commit("insertCart", data);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			return false;
		}
	},
	update: async ({ commit, state }, data) => {
		try {
			const payload = await cartApi.update(data);

			commit("replaceCart", data);
			return true;
		} catch (ex) {
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
			const payload = await cartApi.delete(data);

			const { Message } = payload;

			const newCart = await cartApi.get(state.pagination);

			commit("setCart", newCart);

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
	setCart: (state, payload) => {
		const { Data, ...rest } = payload;

		state.cart = [...Data];
		state.pagination = { ...rest };
	},
	insertCart: (state, newCart) => {
		state.cart.unshift(newCart);
	},
	replaceCart: (state, newCart) => {
		state.cart = state.cart.map((item) => {
			if (item.ProductId === newCart.ProductId) {
				return { ...item, Quantity: newCart.Quantity };
			}

			return item;
		});
	},
	setCurrentItem: (state, cate) => {
		state.currentItem = {
			...cate,
			Thumbnail: "",
			OldThumbnail: cate.Thumbnail,
		};
	},
	clearCurrentItem: (state) => {
		state.currentItem = {};
	},
	clearCart: (state) => {
		state.cart = [];
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
