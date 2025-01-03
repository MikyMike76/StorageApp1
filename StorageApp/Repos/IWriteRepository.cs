﻿using StorageApp.Entities;

namespace StorageApp.Repos
{
    public interface IWriteRepository
    {
        public interface IWriteRepository<in T> where T : class, IEntity
        {
            void Add(T item);
            void Remove(int id);
            void Save();
        }
    }
}
