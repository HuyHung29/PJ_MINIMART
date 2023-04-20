import { useToast } from "vue-toastification";
import categoryApi from "@/api/categoryAPI";

const toast = useToast();

const state = {
	category: [],
	pagination: {
		PageNumber: 1,
		PageSize: 20,
		TotalPage: 0,
		TotalRecord: 0,
	},
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
			// const { Message, Error, UserMes } = ex;
			// toast.error(Message || UserMes);
			// callback.setErrors(Error.MoreInfo);
			// commit("ui/hideLoading", null, { root: true });
		}
	},
};

const mutations = {
	setCategory: (state, payload) => {
		console.log(payload);
		const { Data, ...rest } = payload;

		console.log("djkasgdkjsagdk", Data, rest);
		state.category = [...Data];
		state.pagination = { ...rest };
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
