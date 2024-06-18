using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Unit : IEntity, IEquatable<Unit>
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public int? OKEI { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Unit other) => Id == other.Id;
    }
}
