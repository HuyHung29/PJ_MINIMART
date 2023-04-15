import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const addressApi = {
	get: (data) => axiosClient.post(URL.ADDRESS + "filter", data),
	create: (data) => axiosClient.post(URL.ADDRESS, data),
	update: (id, data) => axiosClient.put(URL.ADDRESS + id, data),
	delete: (data) => axiosClient.delete(URL.ADDRESS, { data }),
};

export default addressApi;
