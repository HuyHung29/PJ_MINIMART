import authApi from "@/api/authAPI";
import userApi from "@/api/userAPI";
import RESOURCE from "@/constants/resource";
import { useToast } from "vue-toastification";

const toast = useToast();

const state = {
	user: localStorage.getItem("user")
		? JSON.parse(localStorage.getItem("user"))
		: {},
	isLogin: localStorage.getItem("token") ? true : false,
	listUser: [],
	pagination: {
		PageSize: 20,
		PageNumber: 1,
		TotalPage: 1,
		TotalRecord: 0,
	},
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
			localStorage.setItem("user", JSON.stringify(User));

			commit("setUser", User);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			console.log(ex);
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			callback.setErrors(Error.MoreInfo);
			commit("ui/hideLoading", null, { root: true });
			return false;
		}
	},
	edit: async ({ commit }, { data, callback }) => {
		try {
			commit("ui/showLoading", null, { root: true });
			const payload = await userApi.edit(data);
			const { Data, Message } = payload;
			toast.success(Message);
			localStorage.setItem("user", JSON.stringify(Data));
			commit("setUser", Data);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			console.log(ex);
			const { Message, Error, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			if (Error) {
				callback.setErrors(Error.MoreInfo);
			}
			commit("ui/hideLoading", null, { root: true });
			return false;
		}
	},
	changePassword: async ({ commit }, data) => {
		try {
			console.log(data);
			commit("ui/showLoading", null, { root: true });
			const payload = await authApi.changePassword(data);
			const { Message } = payload;
			toast.success(Message);
			commit("ui/hideLoading", null, { root: true });
			return true;
		} catch (ex) {
			console.log(ex);
			const { Message, UserMes } = ex;
			toast.error(Message || UserMes || RESOURCE.HELPTEXT);
			commit("ui/hideLoading", null, { root: true });
			return false;
		}
	},
	fetchUser: async ({ commit }, data) => {
		try {
			commit("ui/showLoading", null, { root: true });
			let payload;
			if (data) {
				payload = await userApi.get(data);
			} else {
				payload = await userApi.get(state.pagination);
			}
			commit("loadUser", payload);
			commit("ui/hideLoading", null, { root: true });
		} catch (ex) {
			console.log(ex);

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
		localStorage.clear("user");
	},
	loadUser: (state, users) => {
		const { Data, ...rest } = users;

		state.listUser = [...Data];
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
