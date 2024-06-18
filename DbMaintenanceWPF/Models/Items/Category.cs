using DbMaintenanceWPF.Items.Interfaces;
using System;

namespace DbMaintenanceWPF.Models.Items
{
    public class Category : IEntity, IEquatable<Category>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Category other) => Id == other.Id;
    }
}
