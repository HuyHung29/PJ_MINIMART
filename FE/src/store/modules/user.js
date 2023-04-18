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
	login: async ({ commit }, data) => {
		const payload = await authApi.login(data);
		const { Data, Message } = payload;

		const { AccessToken, User } = Data;

		toast.success(Message);

		localStorage.setItem("token", JSON.stringify(AccessToken));

		commit("setUser", User);
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
