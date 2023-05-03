import { useToast } from "vue-toastification";
import categoryApi from "@/api/categoryAPI";
import RESOURCE from "@/constants/resource";
import { generateFormData } from "@/util/common";

const toast = useToast();

const state = {
	category: [],
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
	fetchCategory: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await categoryApi.get(data);
			} else {
				payload = await categoryApi.get(state.pagination);
			}

			commit("setCategory", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	getCategory: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await categoryApi.get({ CategoryId: data });

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
			const formData = generateFormData(data);

			const payload = await categoryApi.create(formData);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("insertCate", Data);
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

			data = { ...data, CategoryId: state.currentItem.CategoryId };
			const formData = generateFormData(data);
			commit("ui/showLoading", null, { root: true });

			const payload = await categoryApi.update(
				state.currentItem.CategoryId,
				formData
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
			const payload = await categoryApi.delete(data);

			const { Message } = payload;

			const cate = await categoryApi.get(state.pagination);

			commit("setCategory", cate);

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
	setCategory: (state, payload) => {
		const { Data, ...rest } = payload;

		state.category = [...Data];
		state.pagination = { ...rest };
	},
	insertCate: (state, newCate) => {
		state.category.unshift(newCate);
	},
	replaceCate: (state, newCate) => {
		state.category = state.category.map((cate) => {
			if (cate.CategoryId === newCate.CategoryId) {
				return newCate;
			}

			return cate;
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
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
