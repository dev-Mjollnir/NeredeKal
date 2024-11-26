using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeredeKal.RabbitMQ.Models;
using NeredeKal.RabbitMQ.Services;

namespace NeredeKal.RabbitMQ.Extensions
{
    public static class RabbitMQServiceRegisterExtensions
    {
        public static IServiceCollection RegisterRabbitMQSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            return services;
        }

        public static void RegisterRabbitMQService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<RabbitMQClientService>();
        }
    }
}
