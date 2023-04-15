import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const orderApi = {
	get: (data) => axiosClient.post(URL.ORDER + "filter", data),
	create: (data) => axiosClient.post(URL.ORDER, data),
	update: (id, data) => axiosClient.put(URL.ORDER, data),
	delete: (data) => axiosClient.delete(URL.ORDER, { data }),
};

export default orderApi;
