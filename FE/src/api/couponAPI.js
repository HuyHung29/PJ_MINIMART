import axiosClient from "./axiosClient";
import { URL } from "@/constants/resource";

const couponApi = {
	get: (data) => axiosClient.post(URL.COUPON + "filter", data),
	create: (data) => axiosClient.post(URL.COUPON, data),
	update: (id, data) => axiosClient.put(URL.COUPON + id, data),
	delete: (data) => axiosClient.delete(URL.COUPON, { data }),
};

export default couponApi;
