namespace NerdStore.Core.Abstractions;

public interface IMediatrHandler
{
    Task PublishEvent<T>(T publishEvent) where T : Event;
}
