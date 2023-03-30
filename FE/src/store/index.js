import { createLogger, createStore } from "vuex";

const debug = process.env.NODE_ENV !== "production";

export default createStore({
	modules: {},
	strict: debug,
	plugins: debug ? [createLogger()] : [],
});
