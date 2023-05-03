import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const orderApi = {
	get: (data) => axiosClient.post(URL.ORDER + "filter", data),
	getByAccount: (data) =>
		axiosClient.post(URL.ORDER + "get-by-account", data),
	create: (data) => axiosClient.post(URL.ORDER + "add", data),
	update: (data) => axiosClient.put(URL.ORDER + "change-status", data),
	delete: (data) => axiosClient.delete(URL.ORDER, { data }),
};

export default orderApi;
