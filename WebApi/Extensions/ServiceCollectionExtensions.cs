using Application.Interfaces.Services;
using Infrastructure.Services;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServiceLifetimes(this IServiceCollection services)
        {
            services.AddSingleton<IConvertToPdfFileService, ConvertPdfFileService>();

            return services;
        }
    }
}
