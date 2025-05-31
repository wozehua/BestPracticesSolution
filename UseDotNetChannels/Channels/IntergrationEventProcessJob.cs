using MediatR;
using UseDotNetChannels.EventBus;

namespace UseDotNetChannels.Channels
{
    /// <summary>
    /// 消费集成事件
    /// 有了实现生产者的 EventBus,我们需要一种方式来消费发布的 IIntegrationEvent。
    /// 我们可以使用内置的 IHostService 抽象实现一个简单的后台服务。
    /// </summary>
    /// <param name="queue"></param>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="logger"></param>
    internal sealed class IntergrationEventProcessJob(InMemoryMessageQueue queue, IServiceScopeFactory serviceScopeFactory, ILogger<IntergrationEventProcessJob> logger) : BackgroundService
    {
        private static readonly Action<ILogger, Guid, Exception?> executeError = LoggerMessage.Define<Guid>(LogLevel.Error, new EventId(0, nameof(ExecuteAsync)), "Something went wrong! {IntegrationEventId}");
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (IIntergrationEvent intergrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    using IServiceScope scope = serviceScopeFactory.CreateScope();
                    IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                    await publisher.Publish(intergrationEvent, stoppingToken);
                }
                catch (OperationCanceledException opEx)
                {
                    executeError(logger, intergrationEvent.Id, opEx);
                }
                catch (InvalidOperationException invOpEx)
                {
                    executeError(logger, intergrationEvent.Id, invOpEx);
                }
                catch (Exception ex) when (ex is not OperationCanceledException and not InvalidOperationException)
                {
                    executeError(logger, intergrationEvent.Id, ex);
                    throw;
                }
            }
        }
    }
}
