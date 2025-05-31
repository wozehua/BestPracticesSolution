using System.Threading.Channels;
using UseDotNetChannels.EventBus;

namespace UseDotNetChannels.Channels
{
    /// <summary>
    /// 用于异步在生产者和消费者之间传递消息。
    /// Channels 实现了生产者/消费者模式。
    /// 生产者异步产生数据，消费者异步消费这些数据。
    /// 这是构建松散耦合系统的基本模式之一。
    /// 缺点：channels 完全在内存中操作。如果应用崩溃，导致消息丢失。
    /// </summary>
    internal sealed class InMemoryMessageQueue
    {
        /// <summary>
        /// Channel.CreateUnbounded 创建一个无限容量的channel。
        /// </summary>
        private readonly Channel<IIntergrationEvent> channel = Channel.CreateUnbounded<IIntergrationEvent>();

        /// <summary>
        /// 暴露ChannelReader 允许消费者消费消息
        /// </summary>
        public ChannelReader<IIntergrationEvent> Reader => channel.Reader;

        /// <summary>
        /// 暴露ChannelWriter 允许生产者生产消息
        /// </summary>
        public ChannelWriter<IIntergrationEvent> Writer => channel.Writer;
    }
}
