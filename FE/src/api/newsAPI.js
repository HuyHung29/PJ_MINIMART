import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const newsApi = {
	get: (data) => axiosClient.post(URL.NEWS + "filter", data),
	create: (data) => axiosClient.post(URL.NEWS, data),
	update: (id, data) => axiosClient.put(URL.NEWS + id, data),
	delete: (data) => axiosClient.delete(URL.NEWS, { data }),
};

export default newsApi;
