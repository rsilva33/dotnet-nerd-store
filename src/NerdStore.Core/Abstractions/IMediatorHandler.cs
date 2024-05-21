using NerdStore.Core.Messages.CommomMessages.Notifications;

namespace NerdStore.Core.Abstractions;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T publishEvent) where T : Event;
    Task<bool> SendCommand<T>(T command) where T : Command;
    Task PublishNotification<T>(T notification) where T : DomainNotification;
}
