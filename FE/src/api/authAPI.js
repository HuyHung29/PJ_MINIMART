import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const authApi = {
	resiger: (data) => axiosClient.post(URL.AUTH + "register", data),
	login: (data) => axiosClient.post(URL.AUTH + "login", data),
	changePassword: (data) =>
		axiosClient.post(URL.AUTH + "change-password", data),
};

export default authApi;
