using DbMaintenanceWPF.Items.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);
        void AddOnlyList(T item);

        IEnumerable<T> GetAll();

        T Get(int id);

        bool Remove(T item);
        bool RemoveOnlyList(int id);

        void Update(int id, T item);
        void UpdateOnlyList(int id, T item);
    }
}
