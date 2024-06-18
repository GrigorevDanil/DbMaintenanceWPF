using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Purchase : IEntity, IEquatable<Purchase>
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Provider Provider { get; set; }
        public DateTime? DateProvide { get; set; }
        public string TextDateProvide { get { return DateProvide?.ToString("dd.MM.yyyy"); } }
        public int? CountProduct { get; set; }
        public int? Price { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Purchase other) => Id == other.Id;
    }   
}
