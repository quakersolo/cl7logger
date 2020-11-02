using CL7Logger.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CL7Logger.Middleware
{
    internal class CL7LogMiddleware
    {
        private readonly RequestDelegate _next;

        public CL7LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IOptions<CL7LogOptions> options, ICL7LogManager logManager)
        {
            if (httpContext.Request.Path.HasValue &&
                httpContext.Request.Path.Value.ToUpper().StartsWith(options.Value.Path.ToUpper()))
            {
                IQueryCollection queryCollection = httpContext.Request.Query;

                ListLogsParameters listLogsParameters = new ListLogsParameters();

                Guid logEntryId;
                if (queryCollection.ContainsKey("id") && Guid.TryParse(queryCollection["id"], out logEntryId))
                {
                    listLogsParameters.LogEntryId = logEntryId;
                }

                Guid traceId;
                if (queryCollection.ContainsKey("tid") && Guid.TryParse(queryCollection["tid"], out traceId))
                {
                    listLogsParameters.TraceId = traceId;
                }

                int logEntryType;
                if (queryCollection.ContainsKey("ty") &&
                    int.TryParse(queryCollection["ty"], out logEntryType) &&
                    Enum.IsDefined(typeof(CL7LogEntryType), logEntryType))
                {
                    listLogsParameters.LogEntryType = (CL7LogEntryType)logEntryType;
                }

                ListLogsResult listLogsResult = await logManager.ListLogsAsync(listLogsParameters);

                string html = string.Format(@"
<html>
    <head>
        <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"" integrity=""sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"" crossorigin=""anonymous"">
    </head>
    <body>
        <table class=""table table-sm"">
            <thead>
                <tr>
                    <th scope=""col"">Id</th>
                    <th scope=""col"">TraceId</th>
                    <th scope=""col"">Type</th>
                    <th scope=""col"">Message</th>
                    <th scope=""col"">Created at</th>
                    <th scope=""col"">User@Host</th>
                </tr>
            </thead>
            <tbody>
                {0}
            </tbody>
        </table>
    </body>
</html>
", string.Join(Environment.NewLine,
listLogsResult.Items.OrderByDescending(o => o.CreatedAt).Select(item => $"<tr class=\"{GetCssLogEntry(item.LogEntryType)}\"><th scope=\"row\"><a href=\"#\">{item.Id}</a></th><td>{item.TraceId}</td><td>{item.LogEntryType}</td><td>{item.Message}</td><td>{item.CreatedAt:MM/dd/yyyy HH:mm:ss}</td><td>{item.UserId}@{item.Host}</td></tr>")));

                await httpContext.Response.WriteAsync(html);
                return;
            }

            await _next(httpContext);
        }

        private string GetCssLogEntry(CL7LogEntryType cL7LogEntryType)
        {
            switch (cL7LogEntryType)
            {
                case CL7LogEntryType.Information:
                    return "table-info";
                case CL7LogEntryType.Warning:
                    return "table-warning";
                case CL7LogEntryType.Error:
                    return "table-danger";
                default:
                    return string.Empty;
            }
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
