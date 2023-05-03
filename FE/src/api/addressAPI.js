import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const addressApi = {
	get: (data) => axiosClient.post(URL.ADDRESS + "get-all", data),
	create: (data) => axiosClient.post(URL.ADDRESS + "add", data),
	update: (id, data) => axiosClient.put(URL.ADDRESS + "edit/" + id, data),
	delete: (data) => axiosClient.delete(URL.ADDRESS, { data }),
};

export default addressApi;
