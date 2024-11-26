using HotelService.Infrastructure.Data.Context;
using HotelService.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Extensions
{
    public static class RegisterDataModules
    {
        public static void RegisterDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDbContext(services, configuration);
            RegisterRepositories(services);
        }
        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HotelDb");
            services.AddDbContextPool<HotelDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddScoped<IHotelDbContext, HotelDbContext>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();
        }
    }
}
