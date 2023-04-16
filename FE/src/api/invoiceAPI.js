import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const invoiceApi = {
	get: (data) => axiosClient.post(URL.INOICE + "filter", data),
	create: (data) => axiosClient.post(URL.INOICE, data),
	update: (id, data) => axiosClient.put(URL.INOICE + id, data),
	delete: (data) => axiosClient.delete(URL.INOICE, { data }),
};

export default invoiceApi;
