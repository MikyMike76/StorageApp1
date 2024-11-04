using StorageApp.Entities;

namespace StorageApp.Repos.Extensions
{
    public class RepositoryExtensions
    {
        public static void AddBatch<T>(IRepository<T> repository, T[] items) where T : class, IEntity
        {
            foreach (var item in items)
            {
                repository.Add(item);
            }
            repository.Save();
        }

    }
}
