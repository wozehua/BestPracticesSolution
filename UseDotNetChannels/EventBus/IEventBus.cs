namespace UseDotNetChannels.EventBus
{
    /// <summary>
    /// 定义消息抽象
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布消息的抽象接口方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="integrationEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : class, IIntergrationEvent;
    }
}
