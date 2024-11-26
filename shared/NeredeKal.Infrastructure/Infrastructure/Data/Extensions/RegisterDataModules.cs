using NeredeKal.Infrastructure.Infrastructure.Data.Context;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Extensions
{
    public static class RegisterDataModules
    {
        public static void RegisterDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDbContext(services, configuration);
            RegisterRepositories(services);
        }

        public static void UseDataModule(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
            if (context is not null)
                context.Database.Migrate();
        }

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HotelDb");
            services.AddDbContextPool<HotelDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddTransient<IHotelDbContext, HotelDbContext>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
        }
    }
}
