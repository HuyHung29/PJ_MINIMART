import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const supplierApi = {
	get: (data) => axiosClient.post(URL.SUPPLIER + "filter", data),
	create: (data) => axiosClient.post(URL.SUPPLIER, data),
	update: (id, data) => axiosClient.put(URL.SUPPLIER + id, data),
	delete: (data) => axiosClient.delete(URL.SUPPLIER, { data }),
};

export default supplierApi;
