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
            services.AddScoped<IContext, DapperContext>();
            services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
            services.AddScoped<IProductDL, ProductDL>();
            services.AddScoped<IAuthDL, AuthDL>();
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
