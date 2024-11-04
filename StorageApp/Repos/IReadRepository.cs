using StorageApp.Entities;

namespace StorageApp.Repos
{
    public interface IReadRepository 
    {
        public interface IReadRepository<out T> where T : class, IEntity
        {
            IEnumerable<T> GetAll();
            T GetById(int id);
        }
    }
}
