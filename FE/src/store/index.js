import { createLogger, createStore } from "vuex";
import user from "./modules/user";
import cart from "./modules/cart";
import ui from "./modules/ui";
import category from "./modules/category";
import invoice from "./modules/invoice";
import product from "./modules/product";
import order from "./modules/order";
import news from "./modules/news";
import address from "./modules/address";

const debug = process.env.NODE_ENV !== "production";

export default createStore({
	modules: {
		user,
		cart,
		ui,
		category,
		invoice,
		product,
		order,
		news,
		address,
	},
	strict: debug,
	plugins: debug ? [createLogger()] : [],
});
