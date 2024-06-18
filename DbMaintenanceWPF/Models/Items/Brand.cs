using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Brand : IEntity, IEquatable<Brand>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Brand other) => Id == other.Id;
    }
}
