namespace NerdStore.WebApp.MVC.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediatorHandler;

    protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

    protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                             IMediatorHandler mediatorHandler)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
    }

    protected bool ValidatedOperation() =>
        !_notifications.HasNotification();

    protected IEnumerable<string> GetMessagesError() =>
        _notifications.GetNotifications().Select(c => c.Value).ToList();

    protected void NotifyError(string code, string message) =>
        _mediatorHandler.PublishNotification(new DomainNotification(code, message));
}
