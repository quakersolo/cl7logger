using CL7Logger.Common;
using CL7Logger.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CL7Logger.Extensions
{
    public static class LogManagerBuilderHelper
    {
        public static IServiceCollection AddCL7Logger(this IServiceCollection services, Action<CL7LogOptions> setupAction)
        {
            var builder = services.AddSingleton<ConnectionStringManager>();

            builder.AddHttpContextAccessor();
            builder.AddScoped<ICL7LogManager, CL7LogManager>();
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
