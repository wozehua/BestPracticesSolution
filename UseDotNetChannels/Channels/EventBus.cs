using UseDotNetChannels.EventBus;

namespace UseDotNetChannels.Channels
{
    /// <summary>
    /// 实现 EventBus 接口
    /// 使用 InMemoryMessageQueue 来访问 ChannelWriter 并将事件写入通道。
    /// </summary>
    /// <param name="queue"></param>
    internal sealed class EventBus(InMemoryMessageQueue queue) : IEventBus
    {
        public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) where T : class, IIntergrationEvent
        {
            await queue.Writer.WriteAsync(integrationEvent, cancellationToken);
        }
    }
}
