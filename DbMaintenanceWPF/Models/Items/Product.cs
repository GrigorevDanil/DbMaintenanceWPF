using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Unit Unit { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public int CountProduct { get; set; }
        public string? Description { get; set; }
        public string TextProduct { get { return Title + " " + Model; } }
        public bool IsSelected { get; set; }
    }
}
