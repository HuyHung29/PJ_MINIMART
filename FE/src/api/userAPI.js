import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const userApi = {
	get: (data) => axiosClient.post(URL.USER + "filter", data),
	create: (data) => axiosClient.post(URL.USER, data),
	update: (id, data) => axiosClient.put(URL.USER + id, data),
	delete: (data) => axiosClient.delete(URL.USER, { data }),
};

export default userApi;
