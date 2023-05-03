using MINIMART.BL.Helpers;
using MINIMART.BL.IServices;
using MINIMART.BL.Services;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using MINIMART.DL.Repository;

namespace MINIMART.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IContext, DapperContext>();
            services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
            services.AddScoped<IProductDL, ProductDL>();
            services.AddScoped<IOrderDL, OrderDL>();
            services.AddScoped<ICartDL, CartDL>();
            services.AddScoped<IAuthDL, AuthDL>();
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
