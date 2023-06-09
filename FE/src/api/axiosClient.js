import axios from "axios";
import queryString from "query-string";

const axiosClient = axios.create({
	baseURL: import.meta.env.VITE_DEV_BASE_URL,
	headers: {
		"content-type": "application/json",
	},
	paramsSerializer: {
		encode: (params) => queryString.stringify(params),
	},
});
axiosClient.interceptors.request.use(async (config) => {
	// Handle token here ...
	const token = JSON.parse(localStorage.getItem("token"));
	if (token) {
		config.headers.Authorization = `Bearer ${token}`;
	}
	return config;
});
axiosClient.interceptors.response.use(
	(response) => {
		if (response && response.data) {
			return response.data;
		}
		return response;
	},
	(error) => {
		throw error.response.data;
	}
);
export default axiosClient;
