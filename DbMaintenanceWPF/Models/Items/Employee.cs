using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Employee : IEntity, IEquatable<Employee>
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public Post Post { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string NumPhone { get; set; }
        public string Address { get; set; }
        public string TextEmployee { get { return Surname + " " + Name + " " + Lastname; } }

        public bool IsSelected { get; set; }

        public bool Equals(Employee other) => Id == other.Id;
    }
}
