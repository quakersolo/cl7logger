using CL7Logger.Common;
using CL7Logger.Common.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;

namespace CL7Logger.Extensions
{
    public static class LogManagerBuilderHelper
    {
        public static IServiceCollection AddCL7Logger(this IServiceCollection services, Action<LogManagerOptions> setupAction)
        {
            var builder = services.AddSingleton<ConnectionStringManager>();

            builder.AddScoped<ILogManager, LogManager>();
            builder.Configure(setupAction);

            return builder;
        }

        public static IApplicationBuilder UseCL7Logger(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                ConnectionStringManager connectionStringManager = ctx.RequestServices.GetService<ConnectionStringManager>();
                IOptions<LogManagerOptions> options = ctx.RequestServices.GetService<IOptions<LogManagerOptions>>();

                //If its the first time that you use a connectionString then verify if has the tables created!
                if (!connectionStringManager.ConnectionAttempts.Contains(options.Value.ConnectionString))
                {
                    await DatabaseManager.CreateTableIfNotExists(options.Value.ConnectionString);
                    connectionStringManager.ConnectionAttempts.Add(options.Value.ConnectionString);
                }

                //Try to get TraceId value from HttpRequest Header
                StringValues traceValues;
                if (ctx.Request.Headers.TryGetValue(options.Value.TraceIdHeaderName, out traceValues) && traceValues.Count > 0)
                    options.Value.TraceId = Guid.Parse(traceValues[0]);
                else
                    options.Value.TraceId = Guid.NewGuid();

                await next();
            });

            return app;
        }
    }
}
