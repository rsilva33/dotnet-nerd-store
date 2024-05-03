namespace NerdStore.Core.Abstractions.Data;
public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
