using CL7Logger.Application;
using CL7Logger.Application.Interfaces;
using CL7Logger.Core.Common.Interfaces;
using CL7Logger.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

namespace CL7Logger.Core.Common
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

                //If its the first time that you use a connectionString then verify if has the tables created!
                if (!loggerManager.ConnectionAttempts.Contains(options.Value.ConnectionString))
                {
                    ILoggerDbContext loggerDbContext = ctx.RequestServices.GetService<ILoggerDbContext>();

                    await loggerDbContext.EnsureDatabase(options.Value.ConnectionString, new CancellationToken());
                    loggerManager.ConnectionAttempts.Add(options.Value.ConnectionString);
                }

                //Try to get TraceId value from HttpRequest Header: CL7TraceId
                StringValues traceValues;
                if (ctx.Request.Headers.TryGetValue("CL7TraceId", out traceValues) && traceValues.Count > 0)
                    options.Value.TraceId = Guid.Parse(traceValues[0]);
                else
                    options.Value.TraceId = Guid.NewGuid();

                ICL7Logger logger = ctx.RequestServices.GetService<ICL7Logger>();
                await logger.LogAsync($"Starting request logging!", LogLevel.Trace);

                await next();
            });

            return app;
        }
    }
}
