import { useToast } from "vue-toastification";

const toast = useToast();

const state = {
	isLoading: false,
	isOpenSidebar: true,
	modal: {
		isOpen: false,
		title: "",
		type: "",
		content: "",
		callback: null,
		data: null,
	},
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
	handleOpenModal: (state, data) => {
		state.modal = {
			isOpen: true,
			...data,
		};
	},
	handleCloseModal: (state) => {
		state.modal = {
			isOpen: false,
			title: "",
			type: "",
			content: "",
			callback: null,
			data: null,
		};
	},
};

export default { namespaced: true, state, getters, actions, mutations };
