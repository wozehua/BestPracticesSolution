using GlobalErrorHandler.CustomerException;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandler
{
    public sealed class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        private static readonly Action<ILogger, string, Exception> _notFoundError = LoggerMessage.Define<string>(LogLevel.Error, new EventId(1, "NotFoundError"), "Exception occurred: {Message}");

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(exception);
            if (exception is not NotFoundException)
            {
                return false;
            }

            _notFoundError(_logger, exception.Message, exception);
            var problemDetails = new ProblemDetails
            {
                Title = "Resource not found",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
