using CL7Logger.Application.CQRS.Commands.CreateLogEntry;
using Microsoft.Extensions.DependencyInjection;

namespace CL7Logger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ICreateLogEntryCommand, CreateLogEntryCommand>();

            return services;
        }
    }
}
