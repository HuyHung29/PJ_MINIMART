import CustomerLayout from "@/pages/customer/CustomerLayout.vue";
import AdminLayout from "@/pages/admin/AdminLayout.vue";
import AHome from "@/pages/admin/AHome.vue";
import Login from "@/pages/views/Login.vue";
import Register from "@/pages/views/Register.vue";
import HomeVue from "@/pages/customer/Home.vue";
import User from "@/pages/customer/User.vue";
import Profile from "@/pages/customer/components/Profile.vue";
import Address from "@/pages/customer/components/Address.vue";
import Password from "@/pages/customer/components/Password.vue";
import Order from "@/pages/customer/components/Order.vue";
import ACategory from "@/pages/admin/ACategory.vue";
import ASupplier from "@/pages/admin/ASupplier.vue";
import AProduct from "@/pages/admin/AProduct.vue";
import AInvoice from "@/pages/admin/AInvoice.vue";
import AOrder from "@/pages/admin/AOrder.vue";
import AAccount from "@/pages/admin/AAccount.vue";
import ANews from "@/pages/admin/ANews.vue";
import ACoupon from "@/pages/admin/ACoupon.vue";

const routes = [
	{
		path: "/",
		component: CustomerLayout,
		children: [
			{
				path: "",
				component: HomeVue,
			},
			{
				path: "user",
				component: User,
				children: [
					{ path: "profile", component: Profile },
					{
						path: "address",
						component: Address,
					},
					{
						path: "password",
						component: Password,
					},
					{
						path: "Order",
						component: Order,
					},
				],
			},
		],
	},
	{
		path: "/admin",
		component: AdminLayout,
		children: [
			{
				path: "",
				component: AHome,
			},
			{
				path: "category",
				component: ACategory,
			},
			{
				path: "supplier",
				component: ASupplier,
			},
			{
				path: "product",
				component: AProduct,
			},
			{
				path: "invoice",
				component: AInvoice,
			},
			{
				path: "order",
				component: AOrder,
			},
			{
				path: "account",
				component: AAccount,
			},
			{
				path: "news",
				component: ANews,
			},
			{
				path: "coupon",
				component: ACoupon,
			},
		],
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
