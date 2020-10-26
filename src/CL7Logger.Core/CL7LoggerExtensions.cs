using CL7Logger.Application;
using CL7Logger.Application.Interfaces;
using CL7Logger.Core.Interfaces;
using CL7Logger.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Threading;

namespace CL7Logger.Core
{
    public static class CL7LoggerExtensions
    {
        public static IServiceCollection AddCL7Logger(this IServiceCollection services, Action<LoggerOptions> setupAction)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var builder = services.AddSingleton<CL7LoggerManager>();
            builder.AddDbContext<ILoggerDbContext, LoggerDbContext>();
            builder.AddScoped<ICL7Logger, CL7Logger>();
            builder.AddApplicationLayer();

            builder.Configure(setupAction);

            return builder;
        }

        public static IApplicationBuilder UseCL7Logger(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                CL7LoggerManager loggerManager = ctx.RequestServices.GetService<CL7LoggerManager>();
                IOptions<LoggerOptions> options = ctx.RequestServices.GetService<IOptions<LoggerOptions>>();

                if (!loggerManager.ConnectionAttempts.Contains(options.Value.ConnectionString))
                {
                    ILoggerDbContext loggerDbContext = ctx.RequestServices.GetService<ILoggerDbContext>();

                    await loggerDbContext.EnsureDatabase(options.Value.ConnectionString, new CancellationToken());
                    loggerManager.ConnectionAttempts.Add(options.Value.ConnectionString);
                }

                await next();
            });

            return app;
        }
    }
}
