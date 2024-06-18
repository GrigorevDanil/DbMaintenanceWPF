using DbMaintenanceWPF.Items.Interfaces;
using System.Collections.Generic;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface ICreaterEntity<T> where T : IEntity
    {
        List<T> GetList();
        T GetEntity(int id);
    }
}
