namespace NerdStore.Core.Messages;

public abstract class Message
{
    public string MessageType { get; protected set; }
    //raiz de agregacao
    public Guid AggregateId { get; protected set; }

    public Message()
    {
        MessageType = GetType().Name;
    }
}
