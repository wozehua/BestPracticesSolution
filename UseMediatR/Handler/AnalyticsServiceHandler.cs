using MediatR;
using UseMediatR.Notification;

namespace UseMediatR.Handler
{
    /// <summary>
    /// 3:通知分析服务
    /// </summary>
    public class AnalyticsServiceHandler : INotificationHandler<UserRegisteredNotification>
    {
        private const string FormatString = "分析服务：追踪新用户注册：{UserId}";

        private readonly ILogger<AnalyticsServiceHandler> _logger;

        private static readonly Action<ILogger, long, Exception?> _analyticsServiceCalled = LoggerMessage.Define<long>(LogLevel.Information, new EventId(3, nameof(Handle)), FormatString);

        public AnalyticsServiceHandler(ILogger<AnalyticsServiceHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);
            _analyticsServiceCalled(_logger, notification.UserId, null);
            return Task.CompletedTask;
        }
    }
}
