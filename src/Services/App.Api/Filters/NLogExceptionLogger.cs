using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace App.Api.Filters
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            LogError(context);
        }

        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            LogError(context);
            await Task.CompletedTask;
        }


        private void LogError(ExceptionLoggerContext context)
        {
            switch (context.Exception)
            {
                default:
                    Logger.Error(context.Exception, RequestToString(context.Request));
                    break;
            }
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{request.Method} {request.RequestUri} HTTP/{request.Version}");
            stringBuilder.Append(request.Headers);
            stringBuilder.Append(request.Content?.Headers);
            stringBuilder.AppendLine();

            var exist = ScopeContext.TryGetProperty("HttpData", out var data);
            if (exist)
            {
                stringBuilder.AppendLine(data.ToString());
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}
