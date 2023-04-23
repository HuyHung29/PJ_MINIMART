import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const categoryApi = {
	get: (data) => axiosClient.post(URL.CATEGORY + "filter", data),
	create: (data) =>
		axiosClient.post(URL.CATEGORY + "add", data, {
			headers: {
				Accept: "application/json",
				"Content-Type": "multipart/form-data",
			},
		}),
	update: (id, data) =>
		axiosClient.put(URL.CATEGORY + "edit/" + id, data, {
			headers: {
				Accept: "application/json",
				"Content-Type": "multipart/form-data",
			},
		}),
	delete: (data) => axiosClient.delete(URL.CATEGORY, { data }),
};

export default categoryApi;
