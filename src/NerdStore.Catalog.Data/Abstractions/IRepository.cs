namespace NerdStore.Data.Abstractions;

public interface IRepository<T> : IDisposable where T : IAggreageteRoot
{
    IUnitOfWork UnitOfWork { get; }
}
