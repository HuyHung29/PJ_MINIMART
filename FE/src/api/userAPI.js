import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const userApi = {
	get: (data) => axiosClient.post(URL.USER + "filter", data),
	create: (data) => axiosClient.post(URL.USER, data),
	update: (id, data) => axiosClient.put(URL.USER + id, data),
	edit: (data) => axiosClient.put(URL.USER + "edit", data),
	delete: (data) => axiosClient.delete(URL.USER, { data }),
};

export default userApi;
