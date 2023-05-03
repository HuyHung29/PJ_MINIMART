import { useToast } from "vue-toastification";
import newsApi from "@/api/newsAPI";
import RESOURCE from "@/constants/resource";
import { generateFormData } from "@/util/common";

const toast = useToast();

const state = {
	news: [],
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
	fetchNews: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await newsApi.get(data);
			} else {
				payload = await newsApi.get(state.pagination);
			}

			commit("setNews", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);

			commit("ui/hideLoading", null, { root: true });
		}
	},
	getNews: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await newsApi.get({ NewsId: data });

			let news = payload.Data[0];

			commit("setCurrentItem", news);
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

			const payload = await newsApi.create(formData);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("insertNews", Data);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			if (Error || MoreInfo) {
				callback.setErrors(MoreInfo || Error.MoreInfo);
			}
			return false;
		}
	},
	update: async ({ commit, state }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });

			data = { ...data, NewsId: state.currentItem.NewsId };
			const formData = generateFormData(data);
			commit("ui/showLoading", null, { root: true });

			const payload = await newsApi.update(
				state.currentItem.NewsId,
				formData
			);

			const { Data, Message } = payload;

			toast.success(Message);

			commit("replaceNews", Data);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			commit("ui/hideLoading", null, { root: true });
			console.log(ex);
			const { Message, Error, UserMes, MoreInfo } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			if (Error || MoreInfo) {
				callback.setErrors(MoreInfo || Error.MoreInfo);
			}
			return false;
		}
	},
	remove: async ({ commit, state }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });

			console.log(data);
			const payload = await newsApi.delete(data);

			const { Message } = payload;

			const cate = await newsApi.get(state.pagination);

			commit("setNews", cate);

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
	setNews: (state, payload) => {
		const { Data, ...rest } = payload;

		state.news = [...Data];
		state.pagination = { ...rest };
	},
	insertNews: (state, news) => {
		state.news.unshift(news);
	},
	replaceNews: (state, news) => {
		state.news = state.news.map((cate) => {
			if (cate.NewsId === news.NewsId) {
				return news;
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
