import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const productApi = {
	get: (data) => axiosClient.post(URL.PRODUCT + "filter", data),
	create: (data) => axiosClient.post(URL.PRODUCT, data),
	update: (id, data) => axiosClient.put(URL.PRODUCT, data),
	delete: (data) => axiosClient.delete(URL.PRODUCT, { data }),
};

export default productApi;
