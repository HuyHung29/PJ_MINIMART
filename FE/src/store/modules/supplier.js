import { useToast } from "vue-toastification";
import supplierApi from "@/api/supplierAPI";
import RESOURCE from "@/constants/resource";
import { generateFormData } from "@/util/common";

const toast = useToast();

const state = {
	supplier: [],
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
	fetchSupplier: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await supplierApi.get(data);
			} else {
				payload = await supplierApi.get(state.pagination);
			}

			commit("setSupplier", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	getSupplier: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await supplierApi.get({ SupplierId: data });

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

			const payload = await supplierApi.create(data);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("insertSupp", Data);
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

			data = { ...data, SupplierId: state.currentItem.SupplierId };
			commit("ui/showLoading", null, { root: true });

			const payload = await supplierApi.update(
				state.currentItem.SupplierId,
				data
			);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("replaceSupp", Data);
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
			const payload = await supplierApi.delete(data);

			const { Message } = payload;

			const cate = await supplierApi.get(state.pagination);

			commit("setSupplier", cate);

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
	setSupplier: (state, payload) => {
		const { Data, ...rest } = payload;

		state.supplier = [...Data];
		state.pagination = { ...rest };
	},
	insertSupp: (state, newCate) => {
		state.supplier.unshift(newCate);
	},
	replaceSupp: (state, newCate) => {
		state.supplier = state.supplier.map((cate) => {
			if (cate.SupplierId === newCate.SupplierId) {
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
