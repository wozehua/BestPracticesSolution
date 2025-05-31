using MediatR;

namespace UseMediatR.Notification
{
    /// <summary>
    /// 1:定义通知事件
    /// 定义一个通知时间，当用户注册时触发该事件
    /// </summary>
    public class UserRegisteredNotification : INotification
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public UserRegisteredNotification(long userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
