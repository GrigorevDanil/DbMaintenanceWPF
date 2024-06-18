using DbMaintenanceWPF.Items.Interfaces;
using System;

namespace DbMaintenanceWPF.Models.Items
{
    public class Department : IEntity,  IEquatable<Department>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Department other) => Id == other.Id;
    }
}
