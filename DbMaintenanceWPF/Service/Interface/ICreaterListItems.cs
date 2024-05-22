using DbMaintenanceWPF.Items.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface ICreaterListItems<T> where T : IEntity
    {
        List<T> GetList();
    }
}
