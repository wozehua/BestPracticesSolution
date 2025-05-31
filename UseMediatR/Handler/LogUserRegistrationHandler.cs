using MediatR;
using UseMediatR.Notification;

namespace UseMediatR.Handler
{
    /// <summary>
    /// 2.创建处理程序2-记录日志
    /// </summary>
    public class LogUserRegistrationHandler : INotificationHandler<UserRegisteredNotification>
    {
        private const string FormatString = "用户注册：{UserId}, 邮箱：{Email}";
        private readonly ILogger<LogUserRegistrationHandler> _logger;

        private static readonly Action<ILogger, long, string, Exception?> _logUserRegistration = LoggerMessage.Define<long, string>(LogLevel.Information, new EventId(1, "UserRegistration"), FormatString);
        public LogUserRegistrationHandler(ILogger<LogUserRegistrationHandler> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 处理用户注册通知 记录日志
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);
            _logUserRegistration(_logger, notification.UserId, notification.Email, null);
            return Task.CompletedTask;
        }
    }
}
