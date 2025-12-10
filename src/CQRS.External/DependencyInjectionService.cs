using CQRS.Application.ApplicationInsights;
using CQRS.External.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.External
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
            {
                ConnectionString = configuration["CNSTRINGAPPINSIGHTS"]
            });
            services.AddSingleton<IInsertApplicationInsightsService, InsertApplicationInsightsService>();

            return services;
        }

    }
}
