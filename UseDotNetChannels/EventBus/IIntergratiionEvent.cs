using MediatR;
namespace UseDotNetChannels.EventBus
{
    public interface IIntergrationEvent : INotification
    {
        Guid Id { get; init; }
    }

    public abstract record IntergrationEvent(Guid Id) : IIntergrationEvent;
}
