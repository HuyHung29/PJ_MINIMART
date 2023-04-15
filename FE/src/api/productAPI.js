import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const productApi = {
	get: (data) => axiosClient.post(URL.PRODUCT + "filter", data),
	create: (data) => axiosClient.post(URL.PRODUCT, data),
	update: (id, data) => axiosClient.put(URL.PRODUCT, data),
	delete: (data) => axiosClient.delete(URL.PRODUCT, { data }),
};

export default productApi;
