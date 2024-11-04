using Microsoft.EntityFrameworkCore;
using StorageApp.Entities;

namespace StorageApp.Repos
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public EventHandler<T>? itemAdded;
        public EventHandler<T>? itemUpdated;
        public EventHandler<T>? itemRemoved;
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        private const string FileMedicine = "medicinesInFile.txt";
        private const string FileOffers = "offersInFile.txt";

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update (T item)
        {
            var itemToUpdate = _dbSet.Find(item.Id);/*Where(i => i.Id == item.Id).FirstOrDefault();*/
            if (itemToUpdate != null)
            {
                _dbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
            }
            itemUpdated?.Invoke(this, item);
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            using (var writer = File.AppendText(FileMedicine))
            {
                writer.WriteLine(item);
            }
            itemAdded?.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
