﻿namespace NerdStore.Core.Messages;

public abstract class Event : Message, INotification
{
    public DateTime TimeStamp { get; private set; }

    public Event()
    {
        TimeStamp = DateTime.Now;
    }
}
