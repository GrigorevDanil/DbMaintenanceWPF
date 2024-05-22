using DbMaintenanceWPF.Items.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T Get(int id);

        bool Remove(T item);

        void Update(int id, T item);
    }
}
