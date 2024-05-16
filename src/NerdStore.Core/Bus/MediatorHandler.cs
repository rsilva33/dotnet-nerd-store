namespace NerdStore.Core.Bus;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Apenas => algo para disparar
    public async Task PublishEvent<T>(T publishEvent) where T : Event =>
        await _mediator.Publish(publishEvent);

    //Send => request 
    public async Task<bool> SendCommand<T>(T command) where T : Command => 
        await _mediator.Send(command);
}
