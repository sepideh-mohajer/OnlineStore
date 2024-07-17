using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.Interfaces;
using OnlineStore.Business.Interfaces.Base;
using OnlineStore.Business.Services;
using OnlineStore.Business.Services.Base;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.DataAccess.Interfaces.Base;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.DataAccess.Repositories.Base;

namespace OnlineStore.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            // Register other BLL services here

            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            // Register other DAL services here

            return services;
        }
    }
}
