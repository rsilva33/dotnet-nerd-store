namespace NerdStore.Core.Abstractions.Data;

public interface IRepository<T> : IDisposable where T : IAggreageteRoot
{
    IUnitOfWork UnitOfWork { get; }
}
