import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const productApi = {
	get: (data) => axiosClient.post(URL.PRODUCT + "filter", data),
	create: (data) =>
		axiosClient.post(URL.PRODUCT + "add", data, {
			headers: {
				Accept: "application/json",
				"Content-Type": "multipart/form-data",
			},
		}),
	update: (id, data) =>
		axiosClient.put(URL.PRODUCT + "edit/" + id, data, {
			headers: {
				Accept: "application/json",
				"Content-Type": "multipart/form-data",
			},
		}),
	delete: (data) => axiosClient.delete(URL.PRODUCT, { data }),
};

export default productApi;
