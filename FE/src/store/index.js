import { createLogger, createStore } from "vuex";
import user from "./modules/user";
import cart from "./modules/cart";
import ui from "./modules/ui";

const debug = process.env.NODE_ENV !== "production";

export default createStore({
	modules: {
		user,
		cart,
		ui,
	},
	strict: debug,
	plugins: debug ? [createLogger()] : [],
});
