using MediatR;
using UseMediatR.Notification;

namespace UseMediatR.Handler
{
    /// <summary>
    /// 2.创建处理程序
    /// 处理程序负责执行命令或请求，并返回结果。处理程序必须实现INotificationHandler接口
    /// 为每个所需的功能创建独立的处理程序
    /// 发送欢迎邮件
    /// </summary>
    public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredNotification>
    {
        private readonly ILogger<SendWelcomeEmailHandler> _logger;

        private static readonly Action<ILogger, string, Exception?> _loggerInfo = LoggerMessage.Define<string>(LogLevel.Information, new EventId(0, nameof(Handle)), "发送欢迎电子邮件给{Email}");

        public SendWelcomeEmailHandler(ILogger<SendWelcomeEmailHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 处理程序的处理逻辑
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);
            _loggerInfo(_logger, notification.Email, null);
            //模拟发送邮件逻辑
            return Task.CompletedTask;
        }
    }
}
