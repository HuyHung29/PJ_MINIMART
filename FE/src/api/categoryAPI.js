import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const categoryApi = {
	get: (data) => axiosClient.post(URL.CATEGORY + "filter", data),
	create: (data) => axiosClient.post(URL.CATEGORY, data),
	update: (id, data) => axiosClient.put(URL.CATEGORY + id, data),
	delete: (data) => axiosClient.delete(URL.CATEGORY, { data }),
};

export default categoryApi;
