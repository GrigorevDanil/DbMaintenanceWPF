using DbMaintenanceWPF.Items.Interfaces;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbMaintenanceWPF.Service.Base
{
    abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> Items = new();
        protected abstract void Update(T Source, T Destination);
        protected abstract long? AddToDatabase(T item);
        protected abstract void RemoveFromDatabase(T item);
        protected abstract void UpdateInDatabase(int id, T item);

        public T Get(int id) => Items.FirstOrDefault(item => item.Id == id);

        public RepositoryInMemory(List<T> items) => Items = new List<T>(items);

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (Items.Contains(item)) return;

            item.Id = (int)AddToDatabase(item);
            Items.Add(item);
        }

        public IEnumerable<T> GetAll() => Items;

        public bool Remove(T item)
        {
            RemoveFromDatabase(item);
            return Items.Remove(item);
        }

        public void Update(int id, T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Индекс не может быть меньше 1");


            var db_item = ((IRepository<T>)this).Get(id);
            if (db_item is null) throw new InvalidOperationException("Редактируемый элемент не найден в репозитории");

            
            Update(item, db_item);
            UpdateInDatabase(id, db_item);
        }

    }
}
