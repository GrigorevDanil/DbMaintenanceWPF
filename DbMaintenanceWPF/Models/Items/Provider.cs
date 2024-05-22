using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Provider : IEntity
    {
        public int Id { get; set; }
        public string? TitleCompany { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string NumPhone { get; set; }
        public string? Email { get; set; }
        public string TextProvider { get { return TitleCompany == null ? ContactName : TitleCompany + " " + ContactName; } }
        public bool IsSelected { get; set; }
    }
}
