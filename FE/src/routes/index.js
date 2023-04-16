import Home from "@/pages/customer/Home.vue";
import AHome from "@/pages/admin/AHome.vue";
import Login from "@/pages/views/Login.vue";
import Register from "@/pages/views/Register.vue";

const routes = [
	{
		path: "/",
		component: Home,
	},
	{
		path: "/admin",
		component: AHome,
	},
	{
		path: "/login",
		component: Login,
	},
	{
		path: "/register",
		component: Register,
	},
];

export default routes;
