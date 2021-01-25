using CLogger.Common;
using CLogger.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;

namespace CLogger.Extensions
{
    public static class LogManagerBuilderHelper
    {
        public static IServiceCollection AddCL7Logger(this IServiceCollection services, Action<CLogOptions> setupAction)
        {
            var builder = services.AddSingleton<ConnectionStringManager>();

            builder.AddHttpContextAccessor();
            builder.AddScoped<ICLogMonitor, CLogMonitor>();
            builder.Configure(setupAction);

            return builder;
        }

        public static IApplicationBuilder UseCL7Logger(this IApplicationBuilder app)
        {
            app.UseMiddleware<CL7LogMiddleware>();

            return app;
        }
    }
}
