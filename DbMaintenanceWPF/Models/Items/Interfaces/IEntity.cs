using System;

namespace DbMaintenanceWPF.Items.Interfaces
{
    public interface IEntity 
    {
        int Id { get; set; }
        bool IsSelected { get; set; }
    }
}
