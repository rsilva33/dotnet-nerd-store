namespace NerdStore.Core.Abstractions;
public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
