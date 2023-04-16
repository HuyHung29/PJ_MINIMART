import axiosClient from "./axiosClient";
import RESOURCE from "@/constants/resource";

const { URL } = RESOURCE;

const couponApi = {
	get: (data) => axiosClient.post(URL.COUPON + "filter", data),
	create: (data) => axiosClient.post(URL.COUPON, data),
	update: (id, data) => axiosClient.put(URL.COUPON + id, data),
	delete: (data) => axiosClient.delete(URL.COUPON, { data }),
};

export default couponApi;
