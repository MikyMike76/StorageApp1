using static StorageApp.Repos.IReadRepository;
using static StorageApp.Repos.IWriteRepository;
using StorageApp.Entities;

namespace StorageApp.Repos
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
    }
}
