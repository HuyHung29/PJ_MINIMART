import { useToast } from "vue-toastification";

const toast = useToast();

const state = {
	isLoading: false,
	isOpenSidebar: true,
};

const getters = {};

const actions = {};

const mutations = {
	showLoading: (state) => {
		state.isLoading = true;
	},
	hideLoading: (state) => {
		setTimeout(() => {
			state.isLoading = false;
		}, 500);
	},

	openSidebar: (state) => {
		state.isOpenSidebar = true;
	},
	closeSidebar: (state) => {
		state.isOpenSidebar = false;
	},
};

export default { namespaced: true, state, getters, actions, mutations };