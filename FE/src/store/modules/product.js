import { useToast } from "vue-toastification";
import productApi from "@/api/productAPI";
import RESOURCE from "@/constants/resource";
import { generateFormData } from "@/util/common";

const toast = useToast();

const state = {
	product: [],
	pagination: {
		PageNumber: 1,
		PageSize: 20,
		TotalPage: 1,
		TotalRecord: 0,
	},
	currentItem: null,
};

const getters = {};

const actions = {
	fetchProduct: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await productApi.get(data);
			} else {
				payload = await productApi.get(state.pagination);
			}

			commit("setProduct", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	getProduct: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await productApi.get({ ProductId: data });

			let pro = payload.Data[0];

			commit("setCurrentItem", pro);
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
			const formData = generateFormData(data);

			const payload = await productApi.create(formData);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("insertProduct", Data);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			callback.setErrors(MoreInfo || Error.MoreInfo);
		}
	},
	update: async ({ commit, state }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });

			data = { ...data, ProductId: state.currentItem.ProductId };
			const formData = generateFormData(data);
			commit("ui/showLoading", null, { root: true });

			const payload = await productApi.update(
				state.currentItem.ProductId,
				formData
			);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("replaceProduct", Data);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			callback.setErrors(MoreInfo || Error.MoreInfo);
		}
	},
	remove: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			console.log(data);
			const payload = await productApi.delete(data);

			const { Message } = payload;

			const pro = await productApi.get(state.pagination);

			commit("setProduct", pro);

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
	setProduct: (state, payload) => {
		const { Data, ...rest } = payload;

		state.product = [...Data];
		state.pagination = { ...rest };
	},
	insertProduct: (state, newPro) => {
		state.product.unshift(newPro);
	},
	replaceProduct: (state, newPro) => {
		console.log(newPro);
		state.product = state.product.map((pro) => {
			if (pro.ProductId === newPro.ProductId) {
				if (newPro.Pictures) {
					return newPro;
				} else {
					return { ...newPro, Pictures: pro.Pictures };
				}
			}

			return pro;
		});
	},
	setCurrentItem: (state, pro) => {
		state.currentItem = {
			...pro,
			Pictures: "",
			OldPictures: pro.Pictures,
		};
	},
	clearCurrentItem: (state) => {
		state.currentItem = null;
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
