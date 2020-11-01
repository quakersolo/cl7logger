using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CL7Logger.Middleware
{
    internal class CL7LogMiddleware
    {
        private readonly RequestDelegate _next;

        public CL7LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value == ElmahConstants.Path)
            //{
            //    string logConnectionString = await tenantManager.GetLogConnectionStringByUrl(currentUser.Host, new CancellationToken());
            //    //((ElmahLogger)elmahLogger).Configure(logConnectionString, httpContext);
            //}

            //if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value == ElmahConstants.Stylesheet)
            //{
            //    httpContext.Response.Redirect(ElmahConstants.CssMin);
            //}
            //else
            await _next(httpContext);
        }
    }

    public static class MultitenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseMultitenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CL7LogMiddleware>();
        }
    }
}
