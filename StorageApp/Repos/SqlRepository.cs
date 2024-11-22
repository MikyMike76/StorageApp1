using Microsoft.EntityFrameworkCore;
using StorageApp.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace StorageApp.Repos
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public EventHandler<T>? itemAdded;
        public EventHandler<T>? itemUpdated;
        public EventHandler<T>? itemRemoved;
        public EventHandler itemSavedInJson;
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update (T item)
        {
            var itemToUpdate = _dbSet.Where(i => i.Id == item.Id).FirstOrDefault();
            if (itemToUpdate != null)
            {
                _dbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
            }
            itemUpdated?.Invoke(this, item);
        }
        public void Add(T item)
        {
            var lastItem = _dbSet.LastOrDefault();
            if (lastItem != null)
            {
                item.Id = lastItem.Id + 1;

            }
            else
            {
                item.Id = 1;
            }
            _dbSet.Add(item);
            itemAdded.Invoke(this, item);
        }
        public void Remove(int id)
        {
            T itemToRemove = _dbSet.Find(id);
            _dbSet.Remove(itemToRemove);
            itemRemoved.Invoke(this, itemToRemove);
        }
        public void Deserialize()
        {
            var type = _dbSet.GetType();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(path, $"saveFile{type}.json");
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string content = null;
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    content = sr.ReadToEnd();
                }
                var deserializedObject = JsonSerializer.Deserialize<List<T>> (content, options);
                _dbSet.AddRange(deserializedObject);
            }
        }
        public void SaveInFile()
        {
            var type = _dbSet.GetType();
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var serializedObject = JsonSerializer.Serialize<DbSet<T>>(_dbSet, options);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(path, $"saveFile{type}.json");
            using (var sw = new StreamWriter(filePath))
            {
                sw.Write(serializedObject);
            }
            itemSavedInJson.Invoke(this, EventArgs.Empty);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
