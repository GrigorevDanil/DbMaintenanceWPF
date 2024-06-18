using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class GiveDetail : IEntity, IEquatable<GiveDetail>
    {
        public int Id { get; set; }
        public Give Give { get; set; }
        public Product Product { get; set; }
        public int? CountProduct { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(GiveDetail other) => Id == other.Id;
    }
}
