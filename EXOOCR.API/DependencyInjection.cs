using EXOOCR.API;

namespace CODEIT.MMS.Business.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOCRService, OCRService>();

        return services;
    }
}
