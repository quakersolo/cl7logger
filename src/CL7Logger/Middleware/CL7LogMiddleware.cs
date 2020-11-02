using CL7Logger.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CL7Logger.Middleware.Helpers;

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

                string html = string.Format(HTMLHelper.LogHTMLTable,
                    htmlTableRows(listLogsResult, options),
                    options.Value.ApplicationName,
                    options.Value.Path,
                    DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"));

                await httpContext.Response.WriteAsync(html);
                return;
            }

            await _next(httpContext);
        }

        private string htmlTableRows(ListLogsResult listLogsResult, IOptions<CL7LogOptions> options)
        {
            return string.Join(
                        Environment.NewLine,
                        listLogsResult.Items.OrderByDescending(o => o.CreatedAt).
                        Select(item => string.Format(
                            HTMLHelper.LogHTMLRow,
                            getCssLogEntry(item.LogEntryType),
                            item.Id,
                            item.TraceId,
                            item.LogEntryType,
                            item.Message,
                            item.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                            item.UserId,
                            item.Host,
                            options.Value.Path + "?id=" + item.Id,
                            options.Value.Path + "?tid=" + item.TraceId
                        )));
        }

        private string getCssLogEntry(CL7LogEntryType cL7LogEntryType)
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
