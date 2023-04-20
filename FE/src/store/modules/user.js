import authApi from "@/api/authAPI";
import userApi from "@/api/userAPI";
import { useToast } from "vue-toastification";

const toast = useToast();

const state = {
	user: {},
	isLogin: localStorage.getItem("token") ? true : false,
};

const getters = {};

const actions = {
	login: async ({ commit }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });

			const payload = await authApi.login(data);
			const { Data, Message } = payload;

			const { AccessToken, User } = Data;

			toast.success(Message);

			localStorage.setItem("token", JSON.stringify(AccessToken));

			commit("setUser", User);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes);
			callback.setErrors(Error.MoreInfo);
			commit("ui/hideLoading", null, { root: true });
		}
	},
};

const mutations = {
	setUser: (state, user) => {
		state.user = { ...user };
		state.isLogin = true;
	},
	logout: (state) => {
		state.user = {};
		state.isLogin = false;

		localStorage.clear("token");
	},
};

export default {
	namespaced: true,
	state,
	getters,
	actions,
	mutations,
};
