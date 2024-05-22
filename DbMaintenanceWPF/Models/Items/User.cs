using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DbMaintenanceWPF.Models.Items
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public ImageSource Image { get; set; }
        public bool IsLock { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public int CountAttemp { get; set; }
        public DateTime? DateLock { get; set; }
        public string StringDateLock { get { return DateLock?.ToString("dd.MM.yyyy HH:mm"); } }
        public bool IsSelected { get; set; }
    }
}
