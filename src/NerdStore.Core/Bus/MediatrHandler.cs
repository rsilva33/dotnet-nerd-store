namespace NerdStore.Core.Bus;

public class MediatrHandler : IMediatrHandler
{
    private readonly IMediator _mediator;

    public MediatrHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T publishEvent) where T : Event
    {
        await _mediator.Publish(publishEvent);
    }
}
