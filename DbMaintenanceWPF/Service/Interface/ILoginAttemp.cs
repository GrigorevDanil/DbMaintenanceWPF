using DbMaintenanceWPF.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface ILoginAttemp
    {
        void UnlockUser(User user);
        void LockUser(User user);
        void DownAttempUser(User user);
    }
}
