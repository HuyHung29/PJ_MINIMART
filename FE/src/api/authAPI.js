import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const url = "auth/";

const authApi = {
	resiger: (data) => axiosClient.post(URL.AUTH + "register", data),
	login: (data) => axiosClient.post(URL.AUTH + "login", data),
};

export default authApi;
