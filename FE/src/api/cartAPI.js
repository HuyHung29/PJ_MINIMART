import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const cartApi = {
	get: (data) => axiosClient.post(URL.CART + "get-all", data),
	add: (data) => axiosClient.post(URL.CART + "add", data),
	update: (data) => axiosClient.put(URL.CART + "update", data),
	delete: (data) => axiosClient.delete(URL.CART + "delete", { data }),
};

export default cartApi;
