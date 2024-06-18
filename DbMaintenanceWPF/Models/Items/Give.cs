using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Give : IEntity, IEquatable<Give>
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime? DateGive { get; set; }
        public string StringDateGive { get { return DateGive?.ToString("dd.MM.yyyy"); } }
        public string TextGive { get { return Employee.Name[0] + "." + Employee.Lastname[0] + ". " + Employee.Surname + " " + DateGive?.ToString("dd.MM.yyyy"); } }
        public bool IsSelected { get; set; }

        public bool Equals(Give other) => Id == other.Id;
    }
}
