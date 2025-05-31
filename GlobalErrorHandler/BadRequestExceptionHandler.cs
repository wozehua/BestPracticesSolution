using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GlobalErrorHandler.CustomerException;

namespace GlobalErrorHandler
{
    public sealed class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BadRequestExceptionHandler> _logger;

        private static readonly Action<ILogger, string, Exception> _loggerError = LoggerMessage.Define<string>(LogLevel.Error, new EventId(1, "BadRequest"), "Exception occurred: {Message}");
        public BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
        {
            _logger = logger;
        }
        // 这个方法是 BadRequestExceptionHandler 类中用于处理异常的核心方法。
        // 方法名为 TryHandleAsync，返回一个 ValueTask<bool>，表示是否成功处理了异常。
        // 参数包括 httpContext（当前 HTTP 上下文）、exception（捕获到的异常）、cancellationToken（取消令牌）。

        // 首先，方法会检查 httpContext 和 exception 是否为 null，如果是，则抛出 ArgumentNullException，保证后续代码安全执行。
        // 接着判断传入的 exception 是否是 BadRequestException 类型，如果不是，则返回 false，表示本处理器不处理该异常。
        // 如果是 BadRequestException，则执行以下逻辑：

        // 1. 通过 _loggerError 记录一条错误日志，内容为异常的消息和异常对象本身。
        // 2. 使用 httpContext.Response.WriteAsJsonAsync 方法，将一个 ProblemDetails 对象序列化为 JSON 并写入响应体。
        //    ProblemDetails 对象包含：
        //      - Title: "Bad Request"（错误标题）
        //      - Status: 400（HTTP 状态码，表示客户端请求有误）
        //      - Detail: exception.Message（异常的详细信息）
        // 3. 最后返回 true，表示异常已被成功处理。

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // 检查参数是否为 null
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(exception);

            // 只处理 BadRequestException 类型的异常
            if (exception is not BadRequestException)
            {
                return false;
            }

            // 记录错误日志
            _loggerError(_logger, exception.Message, exception);

            // 返回标准的 400 Bad Request 响应
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            }, cancellationToken);

            // 表示异常已被处理
            return true;
        }
    }
}
