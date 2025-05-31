using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandler
{
    public class ExceptionHandingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandingMiddleware> _logger;

        private static readonly Action<ILogger, string, Exception> _errorHandler = LoggerMessage.Define<string>(LogLevel.Error, new EventId(1, nameof(InvokeAsync)), "An error occurred while processing the request: {Message}");

        public ExceptionHandingMiddleware(RequestDelegate next, ILogger<ExceptionHandingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _errorHandler(_logger, ex.Message, ex);
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
